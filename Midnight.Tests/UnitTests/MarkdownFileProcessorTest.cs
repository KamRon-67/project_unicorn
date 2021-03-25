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

namespace Midnight.Tests.UnitTests
{
    public class MarkdownFileProcessorTest
    {
        readonly Mock<IUnicornFileSystem> _fileSystem;
        readonly Fixture _fixture;

        public MarkdownFileProcessorTest()
        {
            _fixture = new Fixture();
            _fileSystem = new Mock<IUnicornFileSystem>();
            _fileSystem.Setup(f => f.GetFiles(It.IsAny<String>())).Verifiable();
            _fileSystem.Setup(f => f.ReadAllTextAsync(It.IsAny<string>())).Returns(Task.CompletedTask as Task<string>);
            _fileSystem.Setup(f => f.WriteOutputFilesAsync(It.IsAny<IEnumerable<OutputFile>>())).Returns(Task.CompletedTask as Task);
            _fileSystem.Setup(f => f.WriteOutputFileAsync(It.IsAny<OutputFile>())).Returns(Task.CompletedTask as Task);

        }

        [Fact]
        public void testGetFilesEmptyFolder()
        {
            //Arrange
            var fileSystem = new UnicornFileSystem();
            var dir = Directory.GetCurrentDirectory();

            //Act
            var result = fileSystem.GetFiles(dir);

            //Assert
            result.Should().NotBeEmpty();
        }

        public void testProcessInputAsync()
        {
            //Arrange
        }
    }
}
