using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Midnight.Core
{
    public class TemplatingEngine
    {
        public TemplatingEngine(string engine, string directory)
        {
            _engine = engine;
            _directory = directory;
        }

        private readonly string _engine;
        private readonly string _directory;

        public string Create()
        {
            try
            {
                if (!Directory.Exists(_directory))
                    Directory.CreateDirectory(_directory);

                if (string.Equals("Razor", _engine, StringComparison.InvariantCultureIgnoreCase))
                {
                    return "Razor templating hasn't been implemented yet";
                }

                if (string.Equals("", _engine, StringComparison.InvariantCultureIgnoreCase))
                {
                    Directory.CreateDirectory(Path.Combine(_directory, @"_posts"));
                    Directory.CreateDirectory(Path.Combine(_directory, @"_layouts"));
                    Directory.CreateDirectory(Path.Combine(_directory, @"css"));
                    Directory.CreateDirectory(Path.Combine(_directory, @"img"));

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, @"rss.xml"), false))
                    {
                    }

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, @"atom.xml"), false))
                    {
                    }

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, @"_layouts\layout.html"), false))
                    {
                    }

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, @"_layouts\post.html"), false))
                    {
                    }

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, @"img\favicon.ico")))
                    {
                    }

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, @"index.md"), false))
                    {
                    }

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, @"about.md"), false))
                    {
                    }

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, string.Format(@"_posts\{0}-myfirstpost.md", DateTime.Today.ToString("yyyy-MM-dd"))), false))
                    {
                    }

                    using (StreamWriter fs = new StreamWriter(Path.Combine(_directory, string.Format(@"css\style.css")), false))
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