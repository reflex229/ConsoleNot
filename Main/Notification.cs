using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using Windows.UI.Notifications;
using static Main.Properties;

namespace Main
{
    public class Notification
    {
        private int _i;
        private Timer _timer;

        public Notification()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                !RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine(ResourceManagerProp.GetString("Sorry_OS", CultureInfoProp));
                Environment.Exit(1);
            }
            else
            {
                _timer = new Timer(IterationTime);
                _timer.Elapsed += OnTimedEvent;
                _timer.AutoReset = true;
                _timer.Enabled = true;
                
                Console.WriteLine(ResourceManagerProp.GetString("Success", CultureInfoProp), IterationTime / 1000, Count);
            }
        }
        
        public Notification(int iterationTime)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                !RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine(ResourceManagerProp.GetString("Sorry_OS", CultureInfoProp));
                Environment.Exit(1);
            }
            else
            {
                _timer = new Timer(iterationTime);
                _timer.Elapsed += OnTimedEvent;
                _timer.AutoReset = true;
                _timer.Enabled = true;
                
                Console.WriteLine(ResourceManagerProp.GetString("Success", CultureInfoProp), iterationTime / 1000, Count);
            }
        }
        
        private void OnTimedEvent(object source, ElapsedEventArgs e)
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
                Console.WriteLine(ResourceManagerProp.GetString("Notification_End", CultureInfoProp), NotificationsCount);
            }
        }

        private void WinNotification()
        {
            var toastXml =
                ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
            var stringElements = toastXml.GetElementsByTagName("text");

            for (var i = 0; i < 2; i++)
            {
                stringElements[i].AppendChild(toastXml.CreateTextNode(TitleAndDesc[i]));
            }

            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("ConsoleNotifier").Show(toast);
        }

        private void LinuxNotification()
        {
            try
            {
                Process.Start("notify-send", $"\"{TitleAndDesc[0]}\" \"{TitleAndDesc[1]}\"");
            }
            catch (Exception)
            {
                Console.WriteLine(ResourceManagerProp.GetString("Notify_Ex", CultureInfoProp));
            }
        }
    }
}