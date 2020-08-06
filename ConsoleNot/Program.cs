using System;
using System.Runtime.InteropServices;
using System.Text;
using static ConsoleNot.Properties;

namespace ConsoleNot
{
    internal static class Program
    {
        private static string[] _arguments;
        private static bool _start = true;
        private static int port;

        private static void Main(string[] args)
        {
            EncodingFix();
            _arguments = args;

            if (args.Length < 1)
            {
                Console.WriteLine(ResourceManager.GetString("There_is_no_", CultureInfo));
                return;
            }
            
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--help":
                        Commands.Help();
                        return;
                    case "--client":
                        Console.WriteLine(ResourceManager.GetString("Enter_Port", CultureInfo));
                        try
                        {
                            port = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(ResourceManager.GetString("Only_Numbers", CultureInfo));
                            return;
                        }
                        Console.WriteLine(ResourceManager.GetString("Enter_IP", CultureInfo));
                        var ip = Console.ReadLine();
                        new Client(port, ip);
                        break;
                    case "-h":
                        ConvertAndSet(i, 0);
                        break;
                    case "-m":
                        ConvertAndSet(i, 1);
                        break;
                    case "-s":
                        ConvertAndSet(i, 2);
                        break;
                    case "-c":
                        try
                        {
                            Count = Convert.ToInt32(args[i + 1]);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(ResourceManager.GetString("Only_Numbers", CultureInfo));
                            _start = false;
                        }
                        break;
                    case "-t":
                        Console.WriteLine(ResourceManager.GetString("Enter_Title", CultureInfo));
                        TitleAndDesc[0] = Console.ReadLine();
                        break;
                    case "-d":
                        Console.WriteLine(ResourceManager.GetString("Enter_Description", CultureInfo));
                        TitleAndDesc[1] = Console.ReadLine();
                        break;
                }
            }

            if (!_start) return;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Commands.WinNotification(
                    IterationTime, Count);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) Commands.LinuxNotification(
                    IterationTime, Count);
            else Console.WriteLine(ResourceManager.GetString("Sorry_OS", CultureInfo));
        }

        private static void ConvertAndSet(int i, int timeNum) //Получаем число из аргумента (с исключением).
        {
            try
            {
                Time[timeNum] = Convert.ToInt32(_arguments[i + 1]);
            }
            catch (FormatException)
            {
                Console.WriteLine(ResourceManager.GetString("Only_Numbers", CultureInfo));
                _start = false;
            }
        }

        private static void EncodingFix()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.OutputEncoding = Encoding.Unicode; Console.InputEncoding = Encoding.Unicode;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) 
            {
                Console.OutputEncoding = Encoding.UTF8; Console.InputEncoding = Encoding.UTF8;
            }
        }
    }
}