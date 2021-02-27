
using System.CommandLine;

namespace Midnight.Core.Models
{
    public static class RootCommandOptions
    {
        public static RootCommand SetOptions()
        {
            var rootCommand = new RootCommand
            {
                new Option<string>(
                    "--create",
                    description: "Create is used to create the folder structure for a new site"),

                new Option<string>(
                    "--generate",
                    "can the current directory for a website and detect the content to process then run")
            };
            return rootCommand;
        }
    }
}