using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediaService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaService.Controllers
{
    [Produces("application/json")]
    [Route("mediaService/{serviceId}/api/v1/")]
    [ApiController]
    public class MediaServiceController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        
        public MediaServiceController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost("upload")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> UploadFile([Required] IFormFile file, string folderPath)
        {
            return Ok(await _mediaService.UploadFile(file, folderPath));
        }

        [HttpGet("downloadFile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DownloadFileByFilePath([FromQuery] string filePath)
        {
            var (stream, fileName) = await _mediaService.DownloadFile(filePath);
            return File(stream, "application/octet-stream", fileName);
        }
        
        [HttpGet("getFilesPaths")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetFilesPaths(string relativeFolderPath)
        {
            return Ok(await _mediaService.GetFilesList(relativeFolderPath));
        }
        

        [HttpDelete("delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteFileByPath([FromQuery] string filePath)
        {
            await _mediaService.DeleteFileByPath(filePath);
            return Ok();
        }
    }
}