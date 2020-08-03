using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace ConsoleNot
{
    public static class Properties
    {
        private static Assembly A => Assembly.Load("ConsoleNot");
        public static ResourceManager ResourceManager => new ResourceManager("ConsoleNot.Lang.langres", A);
        public static CultureInfo CultureInfo => CultureInfo.CurrentCulture;

        public static int[] Time { get; } = {0, 0, 0}; //hours, minutes, seconds
        public static int Count { get; set; } = 1;
        public static int CfgCount { get; set; }

        public static string[] TitleAndDesc { get; } = //0 - title, 1 - description.
        {
            ResourceManager.GetString("Title", CultureInfo),
            ResourceManager.GetString("Title", CultureInfo)
        };

        public static int IterationTime => Time[0] * 3600000 + Time[1] * 60000 + Time[2] * 1000;
        public static int CfgTotalTime { get; set; }

        public static int PreviousTime { get; set; }
        public static bool HasCfg { get; set; }
        public static int TimeDifference => (int) (DateTime.Now.TimeOfDay.TotalSeconds - PreviousTime) * 1000;

        public static Configuration ConfigFile =>
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public static KeyValueConfigurationCollection Settings => ConfigFile.AppSettings.Settings;
        public static NameValueCollection NameValueCollection => ConfigurationManager.AppSettings;
    }
}