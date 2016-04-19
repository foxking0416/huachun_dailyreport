using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace HuaChun_DailyReport
{
    class AppSetting
    {
        public static void SaveSetting(string key, string value)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                configFile.AppSettings.Settings[key].Value = value;
                configFile.Save();
            }
            catch
            {
                AddSetting(key, value);
            }
        }
        public static void AddSetting(string key, string value)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                configFile.AppSettings.Settings.Add(key, value);
                configFile.Save();
            }
            catch
            {
            }
        }
        public static string GetSetting(string key)
        {
            Configuration configFile = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return configFile.AppSettings.Settings[key].Value;
        }
        public static void DeleteSetting(string key)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                configFile.AppSettings.Settings.Remove(key);
                configFile.Save();
            }
            catch
            {
            }
        }
        public static string LoadInitialSetting(string key, string DefaulValue)
        {
            try
            {
                return GetSetting(key);
            }
            catch
            {
                return DefaulValue;
            }
        }
    }
}
