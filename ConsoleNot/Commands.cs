using System;
using System.Threading;
using ConsoleNotLib;
using static ConsoleNot.Properties;

namespace ConsoleNot
{
    public static class Commands
    {
        public static void Help() => Console.WriteLine(ResourceManager.GetString("Commands_Help_", CultureInfo));

        public static void WinNotification(int totalTime, int count)
        {
            Console.WriteLine(ResourceManager.GetString("Success", CultureInfo), totalTime/1000, count);
            for (var i = 0; i < count; i++)
            {
                Thread.Sleep(totalTime);
                CallNot.CallNotification(TitleAndDesc,ResourceManager.GetString("Sorry_OS", CultureInfo),
                    ResourceManager.GetString("Notify_Ex", CultureInfo));
            }
        }
        
        public static void LinuxNotification(int totalTime, int count)
        {
            Console.WriteLine(ResourceManager.GetString("Success", CultureInfo), totalTime/1000, count);
            for (var i = 0; i < count; i++)
            {
                Thread.Sleep(totalTime);
                CallNot.CallNotification(TitleAndDesc,ResourceManager.GetString("Sorry_OS", CultureInfo),
                    ResourceManager.GetString("Notify_Ex", CultureInfo));
            }
        }
    }
}