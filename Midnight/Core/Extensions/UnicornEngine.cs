using System.IO.Abstractions;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Midnight.Core.Extensions.Interfaces;
using Midnight.Core.Extensions.Models;

namespace Midnight.Core.Extensions
{
    public class UnicornEngine
    {
        private List<IFileProcessor> fileProcessors;
        private IUnicornFileSystem unicornFileSystem;
        private string inputDirectory;
        private string outputDirectory;

        public UnicornEngine(IUnicornFileSystem fileSystem, string inputDir, string outputDir)
        {
            this.fileProcessors = new List<IFileProcessor>();

            if (fileSystem == null)
                throw new ArgumentException("File System not provided");

            if (!inputDir.IsSet())
                throw new ArgumentException("Input Directory not provided");

            if (!outputDir.IsSet())
                throw new ArgumentException("OUtput Directory not provided");

            inputDirectory = inputDir;
            outputDirectory = outputDir;
            unicornFileSystem = fileSystem;
        }

        public void AddPrecessor(IFileProcessor processor) => fileProcessors.Add(processor);

        public async Task Generate()
        {
            if (fileProcessors.Any())
                throw new Exception("No processors added to generator.");

            var inputs = unicornFileSystem.GetFiles(inputDirectory);

            var outputFiles = await this.ProcessInputs(inputs, outputDirectory);
            outputFiles = await this.ProcessOutputs(outputFiles);

            await unicornFileSystem.WriteOutputFilesAsync(outputFiles);
        }

        private async Task<IEnumerable<OutputFile>> ProcessInputs(IEnumerable<InputFile> files, string outputDir)
        {
            var outputFiles = new List<OutputFile>();
            foreach (var processor in fileProcessors)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("Processing Input File : " + file.FullPath);
                    var (processed, output) = await processor.ProcessInputAsync(file, outputDir);
                    if (!processed || output == null)
                        continue;

                    outputFiles.Add(output);
                }
            }
        }
            //return outputFiles.GroupBy(x => x.FullPath).Select(x => x.First());
    }

    private async Task<IEnumerable<OutputFile>> ProcessOutputs(IEnumerable<OutputFile> files)
    {
        foreach (var processor in this._fileProcessors)
        {
            foreach (var file in files)
            {
                await processor.ProcessOutputAsync(file);
            }
        }

        return files;
    }
}
