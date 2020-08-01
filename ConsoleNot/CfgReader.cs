using System.Configuration;

namespace ConsoleNot
{
    public class CfgReader
    {

        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        
        public static void SetSetting(string key, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}