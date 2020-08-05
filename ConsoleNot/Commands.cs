using System;
using System.Threading;
using System.Diagnostics;
using Windows.UI.Notifications;
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
                var toastXml =
                    ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
                var stringElements = toastXml.GetElementsByTagName("text");
                for (var j = 0; j < 2; j++)
                {
                    stringElements[j].AppendChild(toastXml.CreateTextNode(TitleAndDesc[j]));
                }
                
                var toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier("ConsoleNotifier").Show(toast);
            }

            Console.Read();
        }
        
        public static void LinuxNotification(int totalTime, int count)
        {
            Console.WriteLine(ResourceManager.GetString("Success", CultureInfo), totalTime/1000, count);
            for (var i = 0; i < count; i++)
            {
                Thread.Sleep(totalTime);
                Process.Start("notify-send", $"\"{TitleAndDesc[0]}\" \"{TitleAndDesc[1]}\"");
            }
        }
    }
}