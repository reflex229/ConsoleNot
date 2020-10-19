using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Web
{
    public static class LangInfo
    {
        public static ResourceManager ResourceManagerProp => new ResourceManager("Web.Lang.langres",
            Assembly.Load("Web"));

        public static CultureInfo CultureInfoProp => CultureInfo.CurrentUICulture;
    }
}