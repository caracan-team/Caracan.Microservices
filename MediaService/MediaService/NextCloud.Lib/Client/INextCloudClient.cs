using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NextCloud.Lib.Client
{
    public interface INextCloudClient
    {
        Task UploadFile(Stream streamFile, string relativeFilePath);
        Task MakeCollection(string folderPath);
        Task Delete(string filePath);
        Task<Stream> DownloadFile(string relativeFilePath);
        Task<IEnumerable<string>> GetFilesList(string relativeFolderPath);
    }
}