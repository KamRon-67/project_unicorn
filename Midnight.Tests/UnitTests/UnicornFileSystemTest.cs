using Midnight.Core.Extensions;
using Xunit;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Midnight.Core.Extensions.Models;

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

        [Fact]
        public async Task TestReadAllTextAsync()
        {
            //Arrange
            string path = Path.GetTempFileName();
            var fi1 = new FileInfo(path);

            // Create a file to write to.
            using (StreamWriter sw = fi1.CreateText())
            {
                sw.WriteLine("Hello");
            }

            var fileSystem = new UnicornFileSystem();

            //Act
            var result = await fileSystem.ReadAllTextAsync(path);


            //Assert
            result.Should().NotBeNullOrEmpty("Hello");
        }


        [Fact]
        public async Task TestWriteOutputFileAsync()
        {
            //Arrange
            var outputFile =  new OutputFile 
            { 
                FullDirectory = Path.GetTempFileName(),
                FullPath = Path.GetTempFileName(),
                Content = "Test1"
            };

            var fileSystem = new UnicornFileSystem();

            //Act
            var result = fileSystem.WriteOutputFileAsync(outputFile);

            FileInfo fileInfo = new FileInfo(outputFile.FullPath);

            //Assert
            fileInfo.Exists.Should().BeTrue();
        }
    }
}
