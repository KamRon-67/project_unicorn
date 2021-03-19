using System;
using Midnight.Core.Extensions;
using Xunit;
using FluentAssertions;
using System.CommandLine;
using Moq;
using System.Collections.Generic;
using Midnight.Core.Extensions.Models;
using Midnight.Core.Extensions.Interfaces;

namespace Midnight.Tests.UnitTests
{
    public class MarkdownFileProcessorTest
    {
        [Fact]
        public void testGetFilesEmptyFolder()
        {
            //Arrange
            var fileSystem = new UnicornFileSystem();

            //Act
            var result = fileSystem.GetFiles(@"");

            //Assert
            result.Should().BeEmpty().And.HaveCount(0);
        }
    }
}
