using AutoMapper;
using FileStorage.Entities;
using FileStorage.Handlers.UploadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<UploadFileDto, File>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Name, opt => opt.Ignore())
                .ForMember(d => d.Type, opt => opt.MapFrom(src => src.FileType));
        }
    }
}
