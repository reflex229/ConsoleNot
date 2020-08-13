using System;

namespace ConsoleNotServer
{
    public class Program
    {
        private static int _port;

        public static void Main (string[] args)
        {
            if (args.Length <= 1)
            {
                Console.WriteLine(Properties.ResourceManager.GetString("Program_No_Arguments",
                    Properties.CultureInfo));
                return;
            }
            for (var i = 0; i < args.Length; i++)
                switch (args[i])
                {
                    case "--help":
                        Console.WriteLine(Properties.ResourceManager.GetString("Program_Help",
                            Properties.CultureInfo));
                        return;
                    case "--port":
                        try
                        {
                            _port = Convert.ToInt32(args[i + 1]);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(Properties.ResourceManager.GetString("Program_Only_Numbers",
                                Properties.CultureInfo));
                        }
                        break;
                }

            var server = new Server(_port);
        }
    }
}