using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
namespace Midnight
{
    class Midnight
    {
        static int Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
             {
                new Option<string>(
                    "--gallop",
                    getDefaultValue: () => "42",
                    description: "An option to scan the current directory and detect the content to process, run"),

                new Option<string>(
                    "--fly",
                    description: "An option to test a site locally")

            };

            rootCommand.Description = "Midnight's test run";

            // Note that the parameters of the handler method are matched according to the names of the options
            rootCommand.Handler = CommandHandler.Create<string>((stringOption) =>
                {
                    Console.WriteLine($"The value for --gallop is: {stringOption}");
                });

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
