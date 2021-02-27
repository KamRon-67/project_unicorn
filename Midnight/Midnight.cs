using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Net;
using Midnight.Core.Server;
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

           // HTTP server port
            int port = 8080;
            if (args.Length > 0)
                port = int.Parse(args[0]);
            // HTTP server content path
            string www = "../../../../../www/api";
            if (args.Length > 1)
                www = args[1];

            Console.WriteLine($"HTTP server port: {port}");
            Console.WriteLine($"HTTP server static content path: {www}");
            Console.WriteLine($"HTTP server website: http://localhost:{port}/api/index.html");

            Console.WriteLine();

            // Create a new HTTP server
            var server = new HttpCacheServer(IPAddress.Any, port);
            server.AddStaticContent(www, "/api");

            // Start the server
            Console.Write("Server starting...");
            server.Start();
            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

            // Perform text input
            for (;;)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    Console.Write("Server restarting...");
                    server.Restart();
                    Console.WriteLine("Done!");
                }
            }

            // Stop the server
            Console.Write("Server stopping...");
            server.Stop();
            Console.WriteLine("Done!");
    });

    // Parse the incoming args and invoke the handler
    return rootCommand.InvokeAsync(args).Result;
}
    }
}
