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
        public static ResourceManager ResourceManager => new ResourceManager("ConsoleNot.Lang.langres", A);
        private static Assembly A => Assembly.Load("ConsoleNot");

        public static CultureInfo CultureInfo => CultureInfo.CurrentCulture;

        private static string[] _arguments;
        private static bool _start = true;

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
                            Commands.Count = Convert.ToInt32(args[i + 1]);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(ResourceManager.GetString("Only_Numbers", CultureInfo));
                            _start = false;
                        }
                        break;
                    case "-t":
                        Console.WriteLine(ResourceManager.GetString("Enter_Title", CultureInfo));
                        Commands.TitleAndDesc[0] = Console.ReadLine();
                        break;
                    case "-d":
                        Console.WriteLine(ResourceManager.GetString("Enter_Description", CultureInfo));
                        Commands.TitleAndDesc[1] = Console.ReadLine();
                        break;
                }
            }

            if (!_start) return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Commands.WinNotification();
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) Commands.LinuxNotification();
            else Console.WriteLine(ResourceManager.GetString("Sorry_OS", CultureInfo));
        }

        private static void ConvertAndSet(int i, int timeNum) //Получаем число из аргумента (с исключением).
        {
            try
            {
                Commands.Time[timeNum] = Convert.ToInt32(_arguments[i + 1]);
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
