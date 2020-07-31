using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using Windows.UI.Notifications;
using XmlDocument = Windows.Data.Xml.Dom.XmlDocument;
using XmlNodeList = Windows.Data.Xml.Dom.XmlNodeList;

namespace ConsoleNot
{
    public static class Commands //Класс, содержащий методы, исполняющие все команды.
    {
        public static int[] _time = {0, 0, 0}; //hours, minutes, seconds
        public static int _count = 1; //Кол-во итераций.
        public static string[] _titleAnddesc = {"Notification", "Notification"}; //0 - заголовок, 1 - описание.
        private static int _totalTime = 0; /*Общее время в секундах.
        Требуется для задержки вывода уведомлений. Вычисляется при помощи массива "Time" в классе "Program". */

        public static void Help() //Вывод команды --help
        {
            Console.WriteLine("-m Sets delay in minutes." +
                              "\n-s Sets delay in seconds." +
                              "\n-h Sets delay in hours." +
                              "\n-t The title of the notification." +
                              "\n-d The description of the notification" +
                              "\n-c The count of iterations (default value is 1)." +
                              "\nYou can always stop the program by pressing Ctrl+C.");
        }

        public static void CalculateTotalTime(int[] time) //hours, minutes, seconds
        {
            _totalTime = (time[0] * 3600000) + (time[1] * 60000) + (time[2] * 1000);
        }

        public static void Notification() //Метод вызова уведомления (в зависимости от ОС пользователя).
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) //Если у пользователя ОС Windows.
            {
                for (int i = 0; i < _count; i++)
                {
                    Thread.Sleep(_totalTime);
                    
                    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
                    XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
                    for (int j = 0; j < 2; j++)
                    {
                        stringElements[j].AppendChild(toastXml.CreateTextNode(_titleAnddesc[j]));
                    }
                    ToastNotification toast = new ToastNotification(toastXml);
                    ToastNotificationManager.CreateToastNotifier("ConsoleNotifier").Show(toast);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) //Если у пользователя ОС Linux.
            {
                Console.WriteLine($"Program has been successfully started. Total delay is {_totalTime/1000} seconds, " +
                                  $"iterations count is {_count}.");
                for (int i = 0; i < _count; i++)
                {
                    Thread.Sleep(_totalTime);
                    Process.Start("notify-send", $"{_titleAnddesc[0]} {_titleAnddesc[1]}");
                }
            }
            else //Если у пользователя не Linux и не Windows, то любезно его посылаем.
            {
                Console.WriteLine("Sorry, your operating system is not supported.");
            }
        }
    }
}