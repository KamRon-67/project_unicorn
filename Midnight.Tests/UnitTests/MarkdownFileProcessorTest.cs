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
using AutoFixture;
using System.Threading.Tasks;
using Midnight.Core.Features.MarkDown;

namespace Midnight.Tests.UnitTests
{
    public class MarkdownFileProcessorTest
    {
        readonly Mock<IUnicornFileSystem> _fileSystem;
        readonly MarkdownFileProcessor _markdownFileProcessor;

        public MarkdownFileProcessorTest()
        {
            _fileSystem = new Mock<IUnicornFileSystem>();
            _markdownFileProcessor = new MarkdownFileProcessor(_fileSystem.Object);
        }

        
        [Theory]
        [InlineData(".m")]
        [InlineData(".txt")]
        [InlineData(".doc")]
        [InlineData(".xls")]
        [InlineData(".jpg")]
        public async Task ReturnsEmptyTupleGivenNonMarkdownFileExtension(string extension)
        {
            //Arrange
            var inputFile = new InputFile
            {
                Extension = extension
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
            string path = Path.GetTempFileName();
            var fi1 = new FileInfo(path);

            // Create a file to write to.
            using (StreamWriter sw = fi1.CreateText())
            {
                sw.WriteLine(" ");
            }

            var inputFile = new InputFile
            {
                Extension = ".md",
                FullPath = path
               
            };

            var fileSystem = new UnicornFileSystem();

            var markdownFileProcessor = new MarkdownFileProcessor(fileSystem);

            //act
            var (processed, file) = await markdownFileProcessor.ProcessInputAsync(inputFile, inputFile.Extension);

            //assert
            processed.Should().BeFalse();
            file.Should().BeNull();
        }
    }
}
