using FileStorage.Handlers.GetFile;
using FileStorage.Handlers.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm]UploadFileDto uploadFileDto)
        {

            return Ok(await _mediator.Send(uploadFileDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetFile([FromQuery]GetFileDto getFileDto)
        {
            var fileViewModel = await _mediator.Send(getFileDto);
            fileViewModel.File.Seek(0, SeekOrigin.Begin);
            return File(fileViewModel.File, fileViewModel.ContentType, $"{fileViewModel.Id}{fileViewModel.Extension}");
        }
    }
}
