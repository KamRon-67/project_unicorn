using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Net;
using System.Threading.Tasks;
using Midnight.Core.Models;
using Midnight.Core.Server;

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
            rootCommand.Handler = CommandHandler.Create<string, string>(DoSomething);

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }

        public static void DoSomething(string create, string generate)
        {
            Console.WriteLine($"The value for --create is: {create}");
            Console.WriteLine($"The value for --generate is: {generate}");

            // HTTP server port
            int port = 8080;

            // HTTP server content path
            string www = "../../../www";


            Console.WriteLine($"HTTP server port: {port}");
            Console.WriteLine($"HTTP server static content path: {www}");
            Console.WriteLine($"HTTP server website: http://localhost:{port}/api/index.html");

            Console.WriteLine();

            // Create a new HTTP server
            var server = new HttpCacheServer(IPAddress.Any, port);
            server.AddStaticContent(www, "/api");

            // Start the server
            StartServer(server);
        }

        public static void StartServer(HttpCacheServer server)
        {
            Console.Write("Server starting...");
            server.Start();
            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");



            //Perform text input
            for (; ; )
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
        }
    }
}
