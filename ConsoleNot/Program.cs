using System;
using System.Runtime.InteropServices;

namespace ConsoleNot
{
    class Program //Основной класс.
    {
        //TODO: Сделать один исполняемый файл при помощи ILMerge.
        private static string[] _arguments;
        private static bool _start = true;
        private const string Error = "Error, you should only enter numbers (ex. -c 10).";

        static void Main(string[] args)
        {
            _arguments = args;
            if (args.Length < 1)
            {
                Console.WriteLine("There is no arguments given." +
                                  " Exiting the program. Please, enter an arguments (see --help) and try again.");
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
                            Console.WriteLine(Error);
                            _start = false;
                        }
                        break;
                    case "-t":
                        Commands._titleAnddesc[0] = args[i + 1];
                        break;
                    case "-d":
                        Commands._titleAnddesc[1] = args[i + 1];
                        break;
                }
            }
            Commands.CalculateTotalTime(Commands._time);

            if (!_start) return;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Commands.WinNotification();
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) Commands.LinuxNotification();
            else Console.WriteLine("Sorry, your operating system is not supported.");
        }

        private static void ConvertAndSet(int i, int timeNum)
        {
            try
            {
                Commands._time[timeNum] = Convert.ToInt32(_arguments[i + 1]);
            }
            catch (FormatException)
            {
                Console.WriteLine(Error);
                _start = false;
            }
        }
    }
}
