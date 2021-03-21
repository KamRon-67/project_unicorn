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
using System.Reflection;

namespace Midnight.Tests.UnitTests
{
    public class MarkdownFileProcessorTest
    {
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
    }
}
