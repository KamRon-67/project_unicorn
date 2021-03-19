using System.Threading.Tasks;
using Midnight.Core.Extensions.Models;
using Midnight.Core.Features.FileSystem;
namespace Midnight.Core.Extensions.Interfaces
{
    public interface IFileProcessor
    {
        Task<(bool processed, OutputFile file)> ProcessInputAsync(InputFile file, string outputDirectory);
        Task ProcessOutputAsync(OutputFile file);
    }
}
