using System;
using System.Configuration;
using static ConsoleNot.Properties;

namespace ConsoleNot
{
    public static class CfgReader
    {
        private static string ReadSetting(string key)
        {
            try
            {
                var result = NameValueCollection[key] ?? "Not Found";
                return result;
            }  
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine(ResourceManager.GetString("Config_Error", CultureInfo));
                return null;
            }
        }

        private static void AddOrUpdate(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;

                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }  
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine(ResourceManager.GetString("Config_Error", CultureInfo));
            }
        }

        public static void Check() //Запись необходимых переменных в файл.
        {
            if (!HasCfg)
            {
                Console.WriteLine("Cfg файла нет.");
                AddOrUpdate("NotificationTime",((int) DateTime.Now.TimeOfDay.TotalSeconds).ToString());
                AddOrUpdate("Title", TitleAndDesc[0]);
                AddOrUpdate("Description", TitleAndDesc[1]);
                AddOrUpdate("Count", Count.ToString());
                AddOrUpdate("IterationTime", IterationTime.ToString());
            }
            else
            {
                Console.WriteLine("Cfg файл есть.");
                TitleAndDesc[0] = ReadSetting("Title");
                TitleAndDesc[1] = ReadSetting("Description");
                CfgTotalTime = Convert.ToInt32(ReadSetting("IterationTime"));
                PreviousTime = Convert.ToInt32(ReadSetting("NotificationTime"));
                CfgCount = Convert.ToInt32(ReadSetting("Count"));
                Console.WriteLine(PreviousTime);
                Console.WriteLine(TimeDifference);
            }
        }

        public static void Del() //Удаление переменных из файла.
        {
            Settings.Clear();
        }
    }
}