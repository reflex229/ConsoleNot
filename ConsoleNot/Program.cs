using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using ConsoleNot.Resource;

namespace ConsoleNot
{
    class Program //Основной класс.
    {
        public static ResourceManager resourceManager;
        private static Assembly a;
        public static CultureInfo cultureInfo;
        private static string[] _arguments;
        private static bool _start = true;

        static void Main(string[] args)
        {
            LangInit();
            
            _arguments = args;
            
            if (args.Length < 1)
            {
                Console.WriteLine(resourceManager.GetString("There_is_no_", cultureInfo));
                return;
            }
            
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i]) //Обработка команд и вызов соответствующего метода из класса "Commands".
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
                            Commands._count = Convert.ToInt32(args[i + 1]);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(resourceManager.GetString("Only_Numbers", cultureInfo));
                            _start = false;
                        }
                        break;
                    case "-t":
                        Console.WriteLine(resourceManager.GetString("Enter_Title", cultureInfo));
                        Commands._titleAnddesc[0] = $"{Console.ReadLine()}";
                        break;
                    case "-d":
                        Console.WriteLine(resourceManager.GetString("Enter_Description", cultureInfo));
                        Commands._titleAnddesc[1] = $"{Console.ReadLine()}";
                        break;
                }
            }
            Commands.CalculateTotalTime(Commands._time);

            if (!_start) return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Commands.WinNotification();
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) Commands.LinuxNotification();
            else Console.WriteLine(resourceManager.GetString("Sorry_OS", cultureInfo));
        }

        private static void ConvertAndSet(int i, int timeNum) //Получаем число из аргумента (с исключением).
        {
            try
            {
                Commands._time[timeNum] = Convert.ToInt32(_arguments[i + 1]);
            }
            catch (FormatException)
            {
                Console.WriteLine(resourceManager.GetString("Only_Numbers", cultureInfo));
                _start = false;
            }
        }

        private static void LangInit() //Языковые инициализации.
        {
            cultureInfo = CultureInfo.CurrentCulture;
            //cultureInfo = new CultureInfo("ru-RU"); //Для тестов.
            a = Assembly.Load("ConsoleNot");
            resourceManager = new ResourceManager("ConsoleNot.Lang.langres", a);
            
            Commands._titleAnddesc = new[]
                {
                    resourceManager.GetString("Title", cultureInfo),
                    resourceManager.GetString("Title", cultureInfo)
                };
        }
    }
}
