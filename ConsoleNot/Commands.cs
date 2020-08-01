using System;
using System.Threading;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace ConsoleNot
{
    public static class Commands //Класс, содержащий методы, исполняющие все команды.
    {
        public static int[] _time = {0, 0, 0}; //hours, minutes, seconds
        public static int _count = 1; //Кол-во итераций.
        public static string[] _titleAnddesc; //0 - заголовок, 1 - описание.
        private static int _totalTime; /*Общее время в секундах.
        Требуется для задержки вывода уведомлений. Вычисляется при помощи массива "Time" в классе "Program". */

        public static void Help() //Вывод команды --help
        {
            Console.WriteLine(Program.resourceManager.GetString("Commands_Help_", Program.cultureInfo));
        }

        public static void CalculateTotalTime(int[] time) //hours, minutes, seconds
        {
            _totalTime = (time[0] * 3600000) + (time[1] * 60000) + (time[2] * 1000);
        }

        public static void WinNotification()
        {
            Console.WriteLine(Program.resourceManager.GetString("Success", Program.cultureInfo), _totalTime/1000, _count);
            for (int i = 0; i < _count; i++)
            {
                Thread.Sleep(_totalTime);

                XmlDocument toastXml =
                    ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
                XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
                for (int j = 0; j < 2; j++)
                {
                    stringElements[j].AppendChild(toastXml.CreateTextNode(_titleAnddesc[j]));
                }

                ToastNotification toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier("ConsoleNotifier").Show(toast);
            }
        }

        public static void LinuxNotification()
        {
            Console.WriteLine(Program.resourceManager.GetString("Success", Program.cultureInfo), _totalTime/1000, _count);
            for (int i = 0; i < _count; i++)
            {
                Thread.Sleep(_totalTime);
                Process.Start("notify-send", $"\"{_titleAnddesc[0]}\" \"{_titleAnddesc[1]}\"");
            }
        }
    }
}