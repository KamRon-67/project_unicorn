using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midnight.Core.Extensions
{
    public class FileHelper
    {
        private readonly string _directory;

        public FileHelper() { }

        private void CreateTemplateFolder(IFileSystem fileSystem, string name)
        {
            fileSystem.Directory.CreateDirectory(fileSystem.Path.Combine(_directory, name));
        }

        private void CreateFolderstructure() { }
    }
}
