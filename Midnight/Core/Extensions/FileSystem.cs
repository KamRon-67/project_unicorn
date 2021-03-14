using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Midnight.Core.Extensions.Interfaces;
using Midnight.Core.Features.Shared;
using System.IO;
using File = Midnight.Core.Features.FileSystem.File;
using System.Threading.Tasks;

namespace Midnight.Core.Extensions
{
    public class FileSystem : IUnicornFileSystem
    {
        public ServiceResult<IEnumerable<File>> GetFiles(string path)
        {
            try
            {
                var directory = new DirectoryInfo(path);
                var files = directory.EnumerateFiles("*.*", SearchOption.AllDirectories).Select(this.MapFileInfoToFile);
                return new ServiceResult<IEnumerable<File>> { Data = files };
            }

            catch(Exception e)
            {
                return new ServiceResult<IEnumerable<File>>(e.Message);
            }
        }

        public async Task<ServiceResult<bool>> WriteFilesAsync(List<File> files)
        {
            return new ServiceResult<bool>();
        }


        private File MapFileInfoToFile(FileInfo info)
        {
            return new File()
            {
                Name = Path.GetFileNameWithoutExtension(info.Name),
                Extension = info.Extension,
                Directory = info.DirectoryName
            };
        }
        
    }
}
