using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Timers;

namespace ConsoleNotLib
{
    public class Notification
    {
        private static Timer _timer;
        private static string Title { get; set; }
        private static string Description { get; set; }
        private static int TotalTime { get; set; }
        private static int Count { get; set; }
        private static int _i = 0;
        private static CultureInfo CultureInfo => CultureInfo.CurrentCulture;

        public Notification(string title, string description, int totalTime, int count)
        {
            Title = title;
            Description = description;
            TotalTime = totalTime;
            Count = count;
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                !RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine(ResourceManager.GetString("Sorry_OS", CultureInfo));
                Environment.Exit(1);
            }
            else
            {
                SetTimer();
            }
        }

        private static void SetTimer()
        {
            _timer = new Timer(TotalTime);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            Console.WriteLine(ResourceManager.GetString("Success", CultureInfo), TotalTime / 1000, Count);
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (_i < Count)
            {
                _i++;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WinNotification();
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    LinuxNotification();
            }
            else
            {
                _timer.Stop();
            }
        }

        private static ResourceManager ResourceManager => new ResourceManager("ConsoleNot.Lang.langres",
            Assembly.Load("ConsoleNot"));

        private static void WinNotification()
        {
            CallNot.CallNotification(new[] {Title, Description}, ResourceManager.GetString("Sorry_OS", CultureInfo),
                ResourceManager.GetString("Notify_Ex", CultureInfo));
        }

        private static void LinuxNotification()
        {
            CallNot.CallNotification(new[] {Title, Description}, ResourceManager.GetString("Sorry_OS", CultureInfo),
                ResourceManager.GetString("Notify_Ex", CultureInfo));
        }
    }
}