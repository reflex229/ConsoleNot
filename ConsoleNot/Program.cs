﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using static ConsoleNot.Properties;

// ReSharper disable ObjectCreationAsStatement

namespace ConsoleNot
{
    internal static class Program
    {
        private static string[] _arguments;
        private static bool _start = true;
        private static int _port;

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
                        Console.WriteLine(ResourceManagerProp.GetString("Enter_Port", CultureInfoProp));
                        try
                        {
                            _port = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(ResourceManagerProp.GetString("Only_Numbers", CultureInfoProp));
                            return;
                        }

                        Console.WriteLine(ResourceManagerProp.GetString("Enter_IP", CultureInfoProp));
                        var ip = Console.ReadLine();
                        new Client(_port, ip);
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
                //Windows implementation...
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Process.Start("/bin/bash",$@"{execPath}/auto_launch.sh {execPath}");
            }
        }
    }
}