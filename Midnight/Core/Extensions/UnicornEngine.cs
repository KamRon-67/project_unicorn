using Midnight.Core.Extensions.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midnight.Core.Extensions
{
    public class UnicornEngine : ITemplateEngine
    {
        public void Process(IFileSystem fileSystem, string folder)
        {
            var outputPath = Path.Combine(folder, "_site");
            fileSystem.Directory.CreateDirectory(outputPath);

            foreach (var file in fileSystem.Directory.GetFiles(folder))
            {
                var fileName = Path.GetFileName(file);
                var newPath = Path.Combine(outputPath, fileName);
                fileSystem.File.WriteAllText(newPath, fileSystem.File.ReadAllText(file));
            }
        }

    }
}
