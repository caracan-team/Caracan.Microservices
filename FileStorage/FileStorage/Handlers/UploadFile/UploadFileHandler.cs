using AutoMapper;
using FileStorage.Contexts;
using FileStorage.Entities;
using MediatR;
using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using File = FileStorage.Entities.File;

namespace FileStorage.Handlers.UploadFile
{
    public class UploadFileHandler : IRequestHandler<UploadFileDto, UploadFileViewModel>
    {
        private readonly MinioClient _client;
        private readonly IMapper _mapper;
        private readonly FileContext _context;

        public UploadFileHandler(MinioClient client, IMapper mapper, FileContext context)
        {
            _client = client;
            _mapper = mapper;
            _context = context;
        }

        public async Task<UploadFileViewModel> Handle(UploadFileDto request, CancellationToken cancellationToken)
        {
            var bucketExists = await _client.BucketExistsAsync(request.FileType ,cancellationToken);
            if (!bucketExists)
                await _client.MakeBucketAsync(request.FileType, cancellationToken: cancellationToken);

            var name = Guid.NewGuid().ToString();
            var stream = request.File.OpenReadStream();

            await _client.PutObjectAsync(request.FileType, name, stream, stream.Length,cancellationToken: cancellationToken);

            var file = _mapper.Map<File>(request);
            file.Name = name;
            file.ContentType = request.File.ContentType;
            file.Extension = Path.GetExtension(request.File.FileName);
            // file.Extension = request.File.Headers.

            await _context.AddAsync(file);

            await _context.SaveChangesAsync();

            return new UploadFileViewModel { Id = $"file.{request.FileType}.{name}.{file.Id}" };
        }
    }
}
