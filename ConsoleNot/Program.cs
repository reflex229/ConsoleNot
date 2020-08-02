using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleNot
{
    static class Program
    {
        private static string[] _arguments;
        private static bool _start = true;

        private static void Main(string[] args)
        {
            EncodingFix();

            _arguments = args;
            
            if (args.Length < 1)
            {
                Console.WriteLine(Properties.ResourceManager.GetString("There_is_no_", Properties.CultureInfo));
                return;
            }
            
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--help":
                        Commands.Help();
                        return;
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
                            Properties.Count = Convert.ToInt32(args[i + 1]);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(Properties.ResourceManager.GetString("Only_Numbers", Properties.CultureInfo));
                            _start = false;
                        }
                        break;
                    case "-t":
                        Console.WriteLine(Properties.ResourceManager.GetString("Enter_Title", Properties.CultureInfo));
                        Properties.TitleAndDesc[0] = Console.ReadLine();
                        break;
                    case "-d":
                        Console.WriteLine(Properties.ResourceManager.GetString("Enter_Description", Properties.CultureInfo));
                        Properties.TitleAndDesc[1] = Console.ReadLine();
                        break;
                }
            }

            if (!_start) return;
            
            CfgReader.Check();
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Commands.WinNotification();
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) Commands.LinuxNotification();
            else Console.WriteLine(Properties.ResourceManager.GetString("Sorry_OS", Properties.CultureInfo));
        }

        private static void ConvertAndSet(int i, int timeNum) //Получаем число из аргумента (с исключением).
        {
            try
            {
                Properties.Time[timeNum] = Convert.ToInt32(_arguments[i + 1]);
            }
            catch (FormatException)
            {
                Console.WriteLine(Properties.ResourceManager.GetString("Only_Numbers", Properties.CultureInfo));
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
