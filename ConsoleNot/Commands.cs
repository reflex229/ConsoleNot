using System;
using System.Threading;
using System.Diagnostics;
using Windows.UI.Notifications;

namespace ConsoleNot
{
    public static class Commands
    {
        public static void Help()
        {
            Console.WriteLine(Properties.ResourceManager.GetString("Commands_Help_", Properties.CultureInfo));
        }

        public static void WinNotification()
        {
            Console.WriteLine(Properties.ResourceManager.
                GetString("Success", Properties.CultureInfo), Properties.TotalTime/1000, Properties.Count);
            for (var i = 0; i < Properties.Count; i++)
            {
                Thread.Sleep(Properties.TotalTime);
                var toastXml =
                    ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
                var stringElements = toastXml.GetElementsByTagName("text");
                for (var j = 0; j < 2; j++)
                {
                    stringElements[j].AppendChild(toastXml.CreateTextNode(Properties.TitleAndDesc[j]));
                }

                ToastNotification toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier("ConsoleNotifier").Show(toast);
            }
        }

        public static void LinuxNotification()
        {
            Console.WriteLine(Properties.ResourceManager.GetString("Success",
                Properties.CultureInfo), Properties.TotalTime/1000, Properties.Count);
            for (var i = 0; i < Properties.Count; i++)
            {
                Thread.Sleep(Properties.TotalTime);
                Process.Start("notify-send", $"\"{Properties.TitleAndDesc[0]}\" \"{Properties.TitleAndDesc[1]}\"");
            }
        }
    }
}