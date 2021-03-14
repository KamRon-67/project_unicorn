using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;
using System;
using System.Linq;
using SystemFile = System.IO.File;
using Midnight.Core.Features.FileSystem;
using Midnight.Core.Features.Shared;

namespace Midnight.Core.Extensions.Interfaces
{
    public interface IUnicornFileSystem
    {
        ServiceResult<IEnumerable<File>> GetFiles(string path);
        Task<ServiceResult<bool>> WriteFilesAsync(List<File> files);
    }
}
