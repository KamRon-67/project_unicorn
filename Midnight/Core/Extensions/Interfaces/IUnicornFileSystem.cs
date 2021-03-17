using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Midnight.Core.Features.FileSystem;
using Midnight.Core.Features.Shared;
using Midnight.Core.Extensions.Models;

namespace Midnight.Core.Extensions.Interfaces
{
    public interface IUnicornFileSystem
    {
        IEnumerable<InputFile> GetFiles(string path);
        Task<string> ReadAllTextAsync(string path);
        Task WriteOutputFilesAsync(IEnumerable<OutputFile> files);
        Task WriteOutputFileAsync(OutputFile file);
    }
}
