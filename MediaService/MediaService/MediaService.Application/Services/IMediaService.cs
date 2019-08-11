using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MediaService.Application.Services
{
    public interface IMediaService
    {
        Task<string> UploadFile(IFormFile file, string folderPath = "/");
        Task DeleteFileByPath(string filePath = "/");
        Task<(Stream, string)> DownloadFile(string filePath = "/");
        Task<IEnumerable<string>> GetFilesList(string folderPath = "/");
    }
}