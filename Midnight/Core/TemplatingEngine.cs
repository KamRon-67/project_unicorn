using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO;
using System.Linq;
using System.Text;

namespace Midnight.Core
{
    public class TemplatingEngine
    {
        readonly IFileSystem fileSystem;

        // <summary>Create MyComponent with the given fileSystem implementation</summary>
        public TemplatingEngine(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public TemplatingEngine() : this(
       fileSystem: new FileSystem())
        {
        }

        public TemplatingEngine(IFileSystem fileSystem, string engine, string directory)
        {
            this.fileSystem = fileSystem;
            _engine = engine;
            _directory = directory;
        }

        private readonly string _engine;
        private readonly string _directory;

        public string Create()
        {
            try
            {// 
             // !Directory.Exists(_directory)
                if (!fileSystem.Directory.Exists(_directory))
                    fileSystem.Directory.CreateDirectory(_directory);

                if (string.Equals("Razor", _engine, StringComparison.InvariantCultureIgnoreCase))
                {
                    return "Razor templating hasn't been implemented yet";
                }

                if (string.Equals("", _engine, StringComparison.InvariantCultureIgnoreCase))
                {
                    fileSystem.Directory.CreateDirectory(fileSystem.Path.Combine(_directory, @"_posts"));
                    fileSystem.Directory.CreateDirectory(fileSystem.Path.Combine(_directory, @"_layouts"));
                    fileSystem.Directory.CreateDirectory(fileSystem.Path.Combine(_directory, @"css"));
                    fileSystem.Directory.CreateDirectory(fileSystem.Path.Combine(_directory, @"img"));

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, @"rss.xml"), false))
                    {
                    }

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, @"atom.xml"), false))
                    {
                    }

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, @"_layouts\layout.html"), false))
                    {
                    }

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, @"_layouts\post.html"), false))
                    {
                    }

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, @"img\favicon.ico")))
                    {
                    }

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, @"index.md"), false))
                    {
                    }

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, @"about.md"), false))
                    {
                    }

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, string.Format(@"_posts\{0}-myfirstpost.md", DateTime.Today.ToString("yyyy-MM-dd"))), false))
                    {
                    }

                    using (var fs = new StreamWriter(fileSystem.Path.Combine(_directory, string.Format(@"css\style.css")), false))
                    {
                    }
                }

                return "template has been created";
            }
            catch (Exception ex)
            {
                return string.Format("Error trying to create template: {0}", ex.Message);
            }
        }
    }
}