using System.Reflection;
using ConfigurationProviderBaseLibrary;

namespace ConfigurationAttribute
{
    public class ConfigurationComponentBase: IConfigurationComponent
    {
        public void SaveSettings()
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                ConfigurationItemAttribute? configurationItemAttribute = propertyInfo.GetCustomAttribute<ConfigurationItemAttribute>();
                if (configurationItemAttribute != null)
                {
                    ConfigurationProviderType providerType = configurationItemAttribute.ProviderType;
                    string settingName = configurationItemAttribute.SettingName;

                    IConfigurationProvider? provider = ConfigurationProviderLoader.providers.GetValueOrDefault(providerType);
                    if (provider == null) 
                    {
                        throw new DllNotFoundException($"Provider with type {providerType.ToDescriptionString()} not found in {ConfigurationProviderLoader.ProvidersDirectory} directory");
                    }

                    provider.SetSetting(settingName, propertyInfo.GetValue(this)?.ToString());
                }
            }
        }
        
        public void LoadSettings()
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                ConfigurationItemAttribute? configurationItemAttribute = propertyInfo.GetCustomAttribute<ConfigurationItemAttribute>();
                if (configurationItemAttribute != null)
                {
                    ConfigurationProviderType providerType = configurationItemAttribute.ProviderType;
                    string settingName = configurationItemAttribute.SettingName;

                    IConfigurationProvider? provider = ConfigurationProviderLoader.providers.GetValueOrDefault(providerType);
                    if (provider == null)
                    {
                        throw new DllNotFoundException($"Provider with type {providerType.ToDescriptionString()} not found in {ConfigurationProviderLoader.ProvidersDirectory} directory");
                    }

                    string? value = provider.GetSetting(settingName);
                    if (value == null)
                    {
                        propertyInfo.SetValue(this, null, null);
                    }
                    else
                    {
                        try
                        {
                            propertyInfo.SetValue(this, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                        }
                        catch (InvalidCastException icex) when (icex.Message == "Invalid cast from 'System.String' to 'System.TimeSpan'.")
                        {
                            propertyInfo.SetValue(this, TimeSpan.Parse(value), null);
                        }
                    }


                }
            }
        }
    }
}
