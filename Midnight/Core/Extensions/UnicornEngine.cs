using System.IO.Abstractions;
using System.IO;

namespace Midnight.Core.Extensions
{
    public class UnicornEngine
    {
        public void Process(IFileSystem fileSystem, string folder)
        {
            var outputPath = Path.Combine(folder, "_site");
            fileSystem.Directory.CreateDirectory(outputPath);

            foreach (var file in fileSystem.Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories))
            {
                var relativePath = file.Replace(folder, "");
                var newPath = Path.Combine(outputPath, relativePath);

                var template = DotLiquid.Template.Parse(fileSystem.File.ReadAllText(file));

                var output = template.Render();

                fileSystem.File.WriteAllText(newPath, output);
            }
        }

    }
}
