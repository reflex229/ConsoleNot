using System;
using static ConsoleNotServer.Properties;

namespace ConsoleNotServer
{
    internal static class Program
    {
        private static int _port;

        private static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine(ResourceManager.GetString("Program_No_Arguments", CultureInfo));
                return;
            }
            for (var i = 0; i < args.Length; i++)
                switch (args[i])
                {
                    case "--help":
                        Console.WriteLine(ResourceManager.GetString("Program_Help", CultureInfo));
                        return;
                    case "--port":
                        try
                        {
                            _port = Convert.ToInt32(args[i + 1]);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(ResourceManager.GetString("Program_Only_Numbers", CultureInfo));
                        }
                        break;
                }

            var server = new Server(_port);
        }
    }
}