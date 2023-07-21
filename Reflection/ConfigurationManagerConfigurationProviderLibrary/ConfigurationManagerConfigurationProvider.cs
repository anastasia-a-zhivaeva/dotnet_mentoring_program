using System.Configuration;
using ConfigurationProviderBaseLibrary;

namespace ConfigurationAttribute
{
    public class ConfigurationManagerConfigurationProvider: IConfigurationProvider
    {
        public string? GetSetting(string settingName)
        {
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings[settingName];
        }

        public void SetSetting(string settingName, string? value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[settingName] == null)
            {
                settings.Add(settingName, value);
            }
            else
            {
                settings[settingName].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}
