using System.IO;
using System.Threading.Tasks;
using Midnight.Core.Extensions;
using Midnight.Core.Features;
using Midnight.Core.Features.Shared;
using Midnight.Core.Extensions.Interfaces;
using Midnight.Core.Extensions.Models;

namespace Midnight.Core.Features.MarkDown
{
    public class MarkdownFileProcessor: IFileProcessor
    {
        private IUnicornFileSystem _fileSystem;

        public MarkdownFileProcessor(IUnicornFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task<(bool processed, OutputFile file)> ProcessInputAsync(InputFile file, string outputDirectory)
        {
            if (!file.Extension.ToLower().Equals(".md"))
                return (false, null);

            var markdown = await this._fileSystem.ReadAllTextAsync(file.FullPath);
            if (!markdown.IsSet())
                return (false, null);

            var html = Markdig.Markdown.ToHtml(markdown);
            var output = OutputFile.FromInputFile(file);
            output.Content = html;
            output.Extension = ".html";
            output.FullDirectory = Path.GetFullPath(outputDirectory + output.RelativeDirectory);
            output.FullPath = Path.Combine(output.FullDirectory, output.Name + output.Extension);

            return (true, output);
        }

        public Task ProcessOutputAsync(OutputFile file) => Task.CompletedTask;
    }
}