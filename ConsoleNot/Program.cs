using System;

namespace ConsoleNot
{
    class Program //Основной класс.
    {
        //TODO: Сделать один исполняемый файл при помощи ILMerge.
        private static string[] _arguments;
        private const string Error = "Error, you should only enter numbers.";

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
            Commands.Notification();
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
            }
        }
    }
}
