using System;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.InteropServices;
using System.Timers;
using Windows.UI.Notifications;
using static ConsoleNot.Properties;

namespace ConsoleNot
{
    public class Notification
    {
        private int _i;
        public int _id { get; set; }
        public Timer _timer { get; set; }
        public string[] _titleAndDesc { get; set; }
        public ResourceManager _resourceManager { get; set; }
        public CultureInfo _cultureInfo { get; set; }
        //TODO: Add a WebInterface and add this properties to DB.

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
                _id = NotificationsCount;

                _titleAndDesc = TitleAndDesc;
                _resourceManager = ResourceManagerProp;
                _cultureInfo = CultureInfoProp;
                
                _timer = new Timer(IterationTime);
                _timer.Elapsed += OnTimedEvent;
                _timer.AutoReset = true;
                _timer.Enabled = true;
                
                Console.WriteLine(ResourceManagerProp.GetString("Success", CultureInfoProp), IterationTime / 1000, Count);
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
                Console.WriteLine(ResourceManagerProp.GetString("Notification_End", CultureInfoProp), _id);
            }
        }

        private void WinNotification()
        {
            var toastXml =
                ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
            var stringElements = toastXml.GetElementsByTagName("text");

            for (var i = 0; i < 2; i++)
            {
                stringElements[i].AppendChild(toastXml.CreateTextNode(_titleAndDesc[i]));
            }

            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("ConsoleNotifier").Show(toast);
        }

        private void LinuxNotification()
        {
            try
            {
                Process.Start("notify-send", $"\"{_titleAndDesc[0]}\" \"{_titleAndDesc[1]}\"");
            }
            catch (Exception)
            {
                Console.WriteLine(_resourceManager.GetString("Notify_Ex", _cultureInfo));
            }
        }
    }
}