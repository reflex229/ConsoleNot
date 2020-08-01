using System;
using System.Threading;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace ConsoleNot
{
    public static class Commands
    {
        public static int[] Time = {0, 0, 0}; //hours, minutes, seconds
        public static int Count = 1;
            public static string[] TitleAnddesc = { Program. ResourceManager. GetString("Title", Program.CultureInfo),
            Program.ResourceManager.GetString("Title", Program.CultureInfo)}; //0 - заголовок, 1 - описание.
        private static int TotalTime => Time[0] * 3600000 + Time[1] * 60000 + Time[2] * 1000;

        public static void Help()
        {
            Console.WriteLine(Program.ResourceManager.GetString("Commands_Help_", Program.CultureInfo));
        }

        public static void WinNotification()
        {
            Console.WriteLine(Program.ResourceManager.
                GetString("Success", Program.CultureInfo), TotalTime/1000, Count);
            for (int i = 0; i < Count; i++)
            {
                Thread.Sleep(TotalTime);
                XmlDocument toastXml =
                    ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
                XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
                for (int j = 0; j < 2; j++)
                {
                    stringElements[j].AppendChild(toastXml.CreateTextNode(TitleAnddesc[j]));
                }

                ToastNotification toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier("ConsoleNotifier").Show(toast);
            }
        }

        public static void LinuxNotification()
        {
            Console.WriteLine(Program.ResourceManager.GetString("Success",
                Program.CultureInfo), TotalTime/1000, Count);
            for (int i = 0; i < Count; i++)
            {
                Thread.Sleep(TotalTime);
                Process.Start("notify-send", $"\"{TitleAnddesc[0]}\" \"{TitleAnddesc[1]}\"");
            }
        }
    }
}