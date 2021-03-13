using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using System;
using System.Linq;
using SystemFile = System.IO.File;

namespace Midnight.Core.Extensions.Interfaces
{
    public interface IUnicornFileSystem : IFileSystem
    {
        ServiceResult<IEnumerable<File>> GetFiles(string path);
        Task<ServiceResult<bool>> WriteFilesAsync(List<File> files);
    }
}
