using Midnight.Core.Extensions;
using Xunit;
using FluentAssertions;
using System.IO;

namespace Midnight.Tests.UnitTests
{
    public class UnicornFileSystemTest
    {
        [Fact]
        public void TestGetFilesEmptyFolder()
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
