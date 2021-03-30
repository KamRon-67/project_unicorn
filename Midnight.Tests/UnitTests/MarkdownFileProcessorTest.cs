using System;
using Midnight.Core.Extensions;
using Xunit;
using FluentAssertions;
using System.CommandLine;
using Moq;
using System.Collections.Generic;
using Midnight.Core.Extensions.Models;
using Midnight.Core.Extensions.Interfaces;
using System.IO;
using System.IO.Abstractions;
using AutoFixture;
using System.Threading.Tasks;
using Midnight.Core.Features.MarkDown;

namespace Midnight.Tests.UnitTests
{
    public class MarkdownFileProcessorTest
    {
        readonly Mock<IUnicornFileSystem> _fileSystem;
        readonly Fixture _fixture;
        readonly MarkdownFileProcessor _markdownFileProcessor;

        public MarkdownFileProcessorTest()
        {
            _fixture = new Fixture();
            _fileSystem = new Mock<IUnicornFileSystem>();
            _fileSystem.Setup(f => f.GetFiles(It.IsAny<String>())).Verifiable();
            _fileSystem.Setup(f => f.ReadAllTextAsync(It.IsAny<string>())).Returns(Task.CompletedTask as Task<string>);
            _fileSystem.Setup(f => f.WriteOutputFilesAsync(It.IsAny<IEnumerable<OutputFile>>())).Returns(Task.CompletedTask as Task);
            _fileSystem.Setup(f => f.WriteOutputFileAsync(It.IsAny<OutputFile>())).Returns(Task.CompletedTask as Task);

            _markdownFileProcessor = new MarkdownFileProcessor(_fileSystem.Object);
        }

        
        [Fact]
        public async Task TestProcessInputExtensionFirstIfAsync()
        {
            //Arrange
            var inputFile = new InputFile
            {
                Extension = ".m"
            };

            var markdownFileProcessor = new MarkdownFileProcessor(_fileSystem.Object);

            //act
            var (processed, file) = await markdownFileProcessor.ProcessInputAsync(inputFile, inputFile.Extension);

            //assert
            processed.Should().BeFalse();
            file.Should().BeNull();
        }

        [Fact]
        public async Task TestProcessInputExtensionSecondIfAsync()
        {
            //Arrange
            var inputFile = new InputFile
            {
                Extension = ".md",
                FullDirectory = ""
               
            };

            var markdownFileProcessor = new MarkdownFileProcessor(_fileSystem.Object);

            //act
            var (processed, file) = await markdownFileProcessor.ProcessInputAsync(inputFile, inputFile.Extension);

            //assert
            processed.Should().BeFalse();
            file.Should().BeNull();
        }
    }
}
