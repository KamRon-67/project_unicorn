using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Midnight.Core.Extensions.Interfaces;
using Midnight.Core.Features.Shared;
using System.IO;
using UnicornFile = Midnight.Core.Features.FileSystem.File;
using System.Threading.Tasks;
using Midnight.Core.Extensions.Models;
using System.Text;

namespace Midnight.Core.Extensions
{
    public class UnicornFileSystem : IUnicornFileSystem
    {
        public IEnumerable<InputFile> GetFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            var files = directory.EnumerateFiles("*.*", SearchOption.AllDirectories).Select(x => this.MapFileInfoToInputFile(x, path));
            return files;
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            const int fileBufferSize = 4096;
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, fileBufferSize, true))
            using (var reader = new StreamReader(fileStream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        public async Task WriteOutputFilesAsync(IEnumerable<OutputFile> files)
        {
            foreach (var file in files)
            {
                await this.WriteOutputFileAsync(file);
            }
        }

        public async Task WriteOutputFileAsync(OutputFile file)
        {
            Directory.CreateDirectory(file.FullDirectory);
            using (var writer = File.CreateText(file.FullPath))
            {
                Console.WriteLine("Writing Output File : " + file.FullPath);
                await writer.WriteLineAsync(file.Content).ConfigureAwait(false);
            }
        }

        public Task DeleteDirectoryAsync(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive: true);
            return Task.CompletedTask;
        }

        private InputFile MapFileInfoToInputFile(FileInfo info, string basePath)
        {
            return new InputFile()
            {
                Name = Path.GetFileNameWithoutExtension(info.Name),
                Extension = info.Extension,
                FullDirectory = info.DirectoryName,
                FullPath = info.FullName,
                RelativeDirectory = info.DirectoryName.Replace(basePath, "")
            };
        }
    }
}