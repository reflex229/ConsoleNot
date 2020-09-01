using System;
using static ConsoleNot.Properties;

namespace ConsoleNot
{
    public static class Commands
    {
        public static void Help() => Console.WriteLine(ResourceManagerProp.GetString("Commands_Help_", CultureInfoProp));
    }
}