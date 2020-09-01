using System;
using static ConsoleNot.Properties;

namespace ConsoleNot
{
    public static class Commands
    {
        public static void Help() => Console.WriteLine(ResourceManager.GetString("Commands_Help_", CultureInfo));
    }
}