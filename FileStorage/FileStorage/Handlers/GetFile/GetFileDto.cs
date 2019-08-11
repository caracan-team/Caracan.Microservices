using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Handlers.GetFile
{
    public class GetFileDto :IRequest<FileViewModel>
    {
        public string Id { get; set; }
    }
}
