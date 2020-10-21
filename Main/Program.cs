using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
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
                    case "--auto-launch":
                        _start = false;
                        SetAutoLaunch();
                        break;
                    case "--remove-auto-launch":
                        _start = false;
                        RemoveAutoLaunch();
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

        private static void SetAutoLaunch()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                /*
                var reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                reg.SetValue("ConsoleNot", ExecPath+@"\");
                reg.Close();
                */
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("/bin/bash",$"{ExecPath}/auto_launch.sh {ExecPath}");
            }
        }

        private static void RemoveAutoLaunch()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("/bin/bash",$"{ExecPath}/auto_launch_remove.sh"); //TODO: Do something in the script.
            }
        }
    }
}