using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Server
{
    public static class Properties
    {
        public static ResourceManager ResourceManager => new ResourceManager("ConsoleNotServer.Lang.langres",
            Assembly.Load("ConsoleNotServer"));
        public static CultureInfo CultureInfo => CultureInfo.CurrentCulture;
        public static Dictionary<string, string> Values { get; set; }
    }
}