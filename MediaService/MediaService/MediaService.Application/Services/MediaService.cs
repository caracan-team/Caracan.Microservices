using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NextCloud.Lib.Client;
using NextCloud.Lib.Exceptions;

namespace MediaService.Application.Services
{
    public class MediaService : IMediaService
    {
        private readonly INextCloudClient _nextCloudClient;
        private readonly ILogger<MediaService> _logger;
        
        public MediaService(INextCloudClient nextCloudClient, ILogger<MediaService> logger)
        {
            _nextCloudClient = nextCloudClient;
            _logger = logger;
        }
        
        public async Task<string> UploadFile(IFormFile file, string folderPath)
        {
            if (file.Length == 0)
                throw new ArgumentException("File cannot be null!");
            
            var filePath = $"{folderPath}/{file.FileName}";
            
            if(string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be empty!");
            
            if(folderPath != null)
                await _nextCloudClient.MakeCollection(folderPath);
            
            await _nextCloudClient.UploadFile(file.OpenReadStream(), $".{filePath}");
            
            return filePath;
        }

        public async Task DeleteFileByPath(string filePath)
        {
            if(String.IsNullOrEmpty(filePath))
                throw new ArgumentException("Path to file cannot be empty!");

            await _nextCloudClient.Delete(filePath);
        }

        public async Task<(Stream, string)> DownloadFile(string filePath)
        {
            if(string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Path to file cannot be empty!");

            var fileName = filePath.Split("/").Last();

            var stream = await _nextCloudClient.DownloadFile($".{filePath}");
            return (stream, fileName);
        }

        public async Task<IEnumerable<string>> GetFilesList(string folderPath)
        {
            if(string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("Folder path cannot be empty.");

            try
            {
                return await _nextCloudClient.GetFilesList($".{folderPath}");
            }
            catch (NextCloudException nextCloudException)
            {
                _logger.LogInformation(nextCloudException.Message);
                return new List<string>();
            }
        }
    }
}