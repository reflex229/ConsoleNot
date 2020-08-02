using System;
using System.Collections.Specialized;
using System.Configuration;

namespace ConsoleNot
{
    public static class CfgReader
    {
        private static NameValueCollection NameValueCollection => ConfigurationManager.AppSettings;

        static CfgReader ()
        {
            if (NameValueCollection.Count != 0)
            {
                ReadSetting("NotificationTime");
            }
        }
        
        public static void ReadAllSettings()
        {
            try
            {
                if (NameValueCollection.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in NameValueCollection.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, NameValueCollection[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine(Properties.ResourceManager.GetString("Config_Error", Properties.CultureInfo));
            }
        }  
  
        public static string ReadSetting(string key)
        {
            try
            {
                var result = NameValueCollection[key] ?? "Not Found";
                return result;
            }  
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine(Properties.ResourceManager.GetString("Config_Error", Properties.CultureInfo));
                return null;
            }
        }
  
        public static void AddUpdate(string key, string value)
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
                Console.WriteLine(Properties.ResourceManager.GetString("Config_Error", Properties.CultureInfo));
            }
        }

        public static void Check()
        {
            if (NameValueCollection.Count == 0)
            {
                var dt = DateTime.Now.TimeOfDay.ToString();
                AddUpdate("NotificationTime", dt);
                AddUpdate("Count", Properties.Count.ToString());
            }
            else
            {
                Properties.PreviousTime = DateTime.Parse(ReadSetting("NotificationTime")).TimeOfDay.ToString();
            }
        }

        public static void Del() //Удаление переменных из файла
        {
            
        }
    }
}

//Сохранить время из файла в переменную, вычислить разницу между настоящим временем и временем в файле.
//Записать задержку в файл.
/*Запуск программы
 Если кол-во элементов в файле = 0, то сохранить
*/