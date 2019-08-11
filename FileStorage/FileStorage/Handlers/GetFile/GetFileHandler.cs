using AutoMapper;
using FileStorage.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileStorage.Handlers.GetFile
{
    public class GetFileHandler : IRequestHandler<GetFileDto, FileViewModel>
    {

        private readonly MinioClient _client;
        private readonly IMapper _mapper;
        private readonly FileContext _context;

        public GetFileHandler(MinioClient client, IMapper mapper, FileContext context)
        {
            _client = client;
            _mapper = mapper;
            _context = context;
        }

        public async Task<FileViewModel> Handle(GetFileDto request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == GetId(request.Id));
            if(file is null)
            {
                //TODO: throw
            }
            var fileViewModel = new FileViewModel
            {
                Id = $"file.{file.Type}.{file.Name}.{file.Id}",
                ContentType = file.ContentType,
                Extension = file.Extension
            };
            await _client.GetObjectAsync(file.Type, file.Name, (stream) => stream.CopyTo(fileViewModel.File));
            

            return fileViewModel;
        }


        private int GetId(string id)
        {
            var segments = id.Split(".");

            return int.Parse(segments.Last());
        }
    }
}
