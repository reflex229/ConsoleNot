using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using static Lib.Times;
using static Main.Properties;

// ReSharper disable ObjectCreationAsStatement

namespace Main
{
    internal static class Program
    {
        private static string[] _arguments;
        private static bool _start = true;

        private static void Main(string[] args)
        {
            EncodingFix();
            _arguments = args;

            if (args.Length < 1)
            {
                Console.WriteLine(ResourceManagerProp.GetString("There_is_no_", CultureInfoProp));
                Console.ReadLine();
                return;
            }

            for (var i = 0; i < args.Length; i++)
                switch (args[i])
                {
                    case "--help":
                        Console.WriteLine(ResourceManagerProp.GetString("Commands_Help_", CultureInfoProp));
                        return;
                    case "--client":
                        Client.Start(new []{""});
                        break;
                    case "-h":
                        ConvertAndSet(i, (int) Hours);
                        break;
                    case "-m":
                        ConvertAndSet(i, (int) Minutes);
                        break;
                    case "-s":
                        ConvertAndSet(i, (int) Seconds);
                        break;
                    case "-c":
                        try
                        {
                            Count = Convert.ToInt32(args[i + 1]);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(ResourceManagerProp.GetString("Only_Numbers", CultureInfoProp));
                            _start = false;
                        }

                        break;
                    case "-t":
                        Console.WriteLine(ResourceManagerProp.GetString("Enter_Title", CultureInfoProp));
                        TitleAndDesc[0] = Console.ReadLine();
                        break;
                    case "-d":
                        Console.WriteLine(ResourceManagerProp.GetString("Enter_Description", CultureInfoProp));
                        TitleAndDesc[1] = Console.ReadLine();
                        break;
                }

            if (!_start) return;
            NotificationsCount++;
            new Notification();
            Console.ReadLine();
        }

        private static void ConvertAndSet(int i, int timeNum) //Получаем число из аргумента (с исключением).
        {
            try
            {
                Time[timeNum] = Convert.ToInt32(_arguments[i + 1]);
            }
            catch (FormatException)
            {
                Console.WriteLine(ResourceManagerProp.GetString("Only_Numbers", CultureInfoProp));
                _start = false;
            }
        }

        private static void EncodingFix()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.InputEncoding = Encoding.Unicode;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
            }
        }
    }
}