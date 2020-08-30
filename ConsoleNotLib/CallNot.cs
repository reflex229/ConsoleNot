using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Windows.UI.Notifications;

namespace ConsoleNotLib
{
    public static class CallNot
    {
        public static void CallNotification(string[] titleAndDesc, string sorryOS, string errString)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Windows(titleAndDesc);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) Linux(titleAndDesc, errString);
            else Console.WriteLine(sorryOS);
        }

        private static void Windows(string[] titleAndDesc)
        {
            var toastXml =
                    ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
                var stringElements = toastXml.GetElementsByTagName("text");

                for (var j = 0; j < 2; j++)
                {
                    stringElements[j].AppendChild(toastXml.CreateTextNode(titleAndDesc[j]));
                }

                var toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier("ConsoleNotifier").Show(toast);
            
            }

        private static void Linux(string[] titleAndDesc, string errString)
        {
            try
            {
                Process.Start("notify-send", $"\"{titleAndDesc[0]}\" \"{titleAndDesc[1]}\"");
            }
            catch (Exception)
            {
                Console.WriteLine(errString);
            }
        }
    }
}