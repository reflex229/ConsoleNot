using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Main
{
    public static class Properties
    {
        public static ResourceManager ResourceManagerProp => new ResourceManager("Main.Lang.langres",
            Assembly.Load("Main"));

        public static CultureInfo CultureInfoProp => CultureInfo.CurrentUICulture;
        public static int[] Time { get; } = {0, 0, 0}; //hours, minutes, seconds
        public static int Count { get; set; } = 1;
        public static string[] TitleAndDesc { get; } = //0 - title, 1 - description.
        {
            ResourceManagerProp.GetString("Title", CultureInfoProp),
            ResourceManagerProp.GetString("Title", CultureInfoProp)
        };
        public static int IterationTime => Time[0] * 3600000 + Time[1] * 60000 + Time[2] * 1000;
        public static Dictionary<string, string> NotificationValues =>
            new Dictionary<string, string>
            {
                {"Title", TitleAndDesc[0]},
                {"Description", TitleAndDesc[1]},
                {"IterationTime", IterationTime.ToString()},
                {"Count", Count.ToString()}
            };
        public static int NotificationsCount { get; set; }
        public static string ExecPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}