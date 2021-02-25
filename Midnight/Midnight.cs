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
            description: "An option whose argument is parsed as an int"),
        new Option<string>(
            "--fly",
            "An option whose argument is parsed as a bool")
    };

    rootCommand.Description = "My sample app";

   // Note that the parameters of the handler method are matched according to the names of the options
   rootCommand.Handler = CommandHandler.Create<string, string>((gallop, fly) =>
    {
        Console.WriteLine($"The value for --gallop is: {gallop}");
        Console.WriteLine($"The value for --fly is: {fly}");
    });

    // Parse the incoming args and invoke the handler
    return rootCommand.InvokeAsync(args).Result;
}
    }
}
