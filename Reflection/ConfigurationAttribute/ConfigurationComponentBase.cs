using System.Reflection;

namespace ConfigurationAttribute
{
    public class ConfigurationComponentBase: IConfigurationComponent
    {
        public void SaveSettings()
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                ConfigurationItemAttribute configurationItemAttribute = propertyInfo.GetCustomAttribute<ConfigurationItemAttribute>();
                if (configurationItemAttribute != null)
                {
                    Type providerType = configurationItemAttribute.ProviderType;
                    string settingName = configurationItemAttribute.SettingName;

                    var provider = (IConfigurationProvider)Activator.CreateInstance(providerType);
                    provider.SetSetting(settingName, propertyInfo.GetValue(this)?.ToString());
                }
            }
        }
        
        public void LoadSettings()
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                ConfigurationItemAttribute configurationItemAttribute = propertyInfo.GetCustomAttribute<ConfigurationItemAttribute>();
                if (configurationItemAttribute != null)
                {
                    Type providerType = configurationItemAttribute.ProviderType;
                    string settingName = configurationItemAttribute.SettingName;

                    var provider = (IConfigurationProvider)Activator.CreateInstance(providerType);

                    var value = provider.GetSetting(settingName);
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
