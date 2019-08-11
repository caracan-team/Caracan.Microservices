using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Handlers.GetFile
{
    public class FileViewModel
    {
        public MemoryStream File { get; set; } = new MemoryStream();
        public string Id { get; set; }
        public string ContentType { get; set; }
        public string Extension { get; set; }
    }
}
