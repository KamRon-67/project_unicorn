using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using Midnight.Core.Models;
namespace Midnight
{
    class Midnight
    {
        static int Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = RootCommandOptions.SetOptions();

            rootCommand.Description = "Simple static site generator";

            // Note that the parameters of the handler method are matched according to the names of the options
            rootCommand.Handler = CommandHandler.Create<string, string>((create, generate) =>
             {
                 Console.WriteLine($"The value for --create is: {create}");
                 Console.WriteLine($"The value for --generate is: {generate}");
             });

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
