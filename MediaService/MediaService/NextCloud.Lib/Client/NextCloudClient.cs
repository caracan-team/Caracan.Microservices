using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NextCloud.Lib.Configuration;
using NextCloud.Lib.Exceptions;
using WebDav;

namespace NextCloud.Lib.Client
{
    public class NextCloudClient : INextCloudClient
    {
        private readonly WebDavClientParams _webDavClientParams;
        private readonly ILogger<NextCloudClient> _logger;
        
        public NextCloudClient(NextCloudConfig nextCloudConfig, ILogger<NextCloudClient> logger)
        {
            _webDavClientParams = new WebDavClientParams
            {
                BaseAddress = new Uri($"http://{nextCloudConfig.Host}:{nextCloudConfig.Port}/remote.php/dav/files/{nextCloudConfig.UserName}/"),
                Credentials = new NetworkCredential(nextCloudConfig.UserName, nextCloudConfig.Password)
            };
            
            _logger = logger;
        }
        
        public async Task UploadFile(Stream streamFile, string relativeFilePath)
        {
            using (var client = new WebDavClient(_webDavClientParams))
            {
                var res = await client.PutFile(relativeFilePath, streamFile);

                if (res.StatusCode == 200 ||
                    res.StatusCode == 201 ||
                    res.StatusCode == 204)
                {
                    return;
                }
                
                throw new NextCloudException($"Could not upload file! ({res.StatusCode} {res.Description})");
            }
        }

        public async Task MakeCollection(string folderPath)
        {
            using (var client = new WebDavClient(_webDavClientParams))
            {
                var pathFolders = folderPath.Split("/");
                var url = "";
                
                foreach (var folder in pathFolders)
                {
                    url = url.Equals("") ? folder : $"{url}/{folder}/";

                    var res = await client.Mkcol(url);
                    
                    if(res.StatusCode == 200 ||
                       res.StatusCode == 201 ||
                       res.StatusCode == 405)
                    {
                        continue;
                    }

                    var msg = $"Could not create folder {folderPath}! ({res.StatusCode} {res.Description})";
                    
                    _logger.LogError(msg);
                    throw new NextCloudException(msg);
                }
            }
        }

        public async Task Delete(string relativeFilePath)
        {
            using (var client = new WebDavClient(_webDavClientParams))
            {
                var res = await client.Delete(relativeFilePath);

                if (res.StatusCode == 404)
                    throw new FileNotFoundException($"Not found {relativeFilePath}.");

                if (res.StatusCode != 204)
                {
                    var msg = $"Cannot delete object! ({res.StatusCode} {res.Description})";
                        
                    _logger.LogError(msg);
                    throw new ArgumentException(msg);
                }
            }
        }

        public async Task<Stream> DownloadFile(string relativeFilePath)
        {
            using (var client = new WebDavClient(_webDavClientParams))
            {
                var res = await client.GetProcessedFile(relativeFilePath);
                
                if (!res.IsSuccessful || res.StatusCode == 404)
                    throw new FileNotFoundException($"Cannot find file {relativeFilePath}");

                return res.Stream;
            }
        }

        public async Task<IEnumerable<string>> GetFilesList(string relativeFolderPath)
        {
            using (var client = new WebDavClient(_webDavClientParams))
            {
                var res = await client.Propfind(relativeFolderPath);
                
                if (!res.IsSuccessful)
                    throw new NextCloudException($"Cannot find folder {relativeFolderPath}");

                return res.Resources
                    .Select(x =>
                    {
                        var parts = x.Uri.Split("/").Skip(5);
                        return string.Join("/", parts);
                    })
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => $"/{x}");
            }
        }
    }
}