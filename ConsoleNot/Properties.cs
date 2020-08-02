using System.Globalization;
using System.Reflection;
using System.Resources;

namespace ConsoleNot
{
    public class Properties
    {
        public static string PreviousTime { get; set; }
        private static Assembly A => Assembly.Load("ConsoleNot");
        public static ResourceManager ResourceManager => new ResourceManager("ConsoleNot.Lang.langres", A);
        public static CultureInfo CultureInfo => CultureInfo.CurrentCulture;
        public static int[] Time { get; } = {0, 0, 0}; //hours, minutes, seconds
        public static int Count { get; set; } = 1;
        public static string[] TitleAndDesc { get; } = //0 - заголовок, 1 - описание.
        {
            Properties.ResourceManager.GetString("Title", Properties.CultureInfo),
            Properties.ResourceManager.GetString("Title", Properties.CultureInfo)
        };
        public static int TotalTime => Time[0] * 3600000 + Time[1] * 60000 + Time[2] * 1000;
    }
}