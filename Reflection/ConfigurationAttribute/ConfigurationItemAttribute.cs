namespace ConfigurationAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute: Attribute
    {
        private readonly string _settingName;
        private readonly ConfigurationProviderType _providerType;

        public ConfigurationItemAttribute(string settingName, ConfigurationProviderType providerType) 
        {
            _settingName = settingName;
            _providerType = providerType;
        }

        public string SettingName { get => _settingName; }
        public ConfigurationProviderType ProviderType { get => _providerType; }
    }
}
