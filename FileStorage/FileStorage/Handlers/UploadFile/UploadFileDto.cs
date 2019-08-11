using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Handlers.UploadFile
{
    public class UploadFileDto : IRequest<UploadFileViewModel>
    {
        [FromForm]
        public IFormFile File { get; set; }
        [FromForm]
        public string FileType { get; set; }
    }
}
