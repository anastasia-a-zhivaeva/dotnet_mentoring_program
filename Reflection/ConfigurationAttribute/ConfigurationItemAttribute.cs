namespace ConfigurationAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute: Attribute
    {
        private readonly string _settingName;
        private readonly Type _providerType;

        public ConfigurationItemAttribute(string settingName, Type providerType) 
        {
            _settingName = settingName;
            _providerType = providerType;
        }

        public string SettingName { get => _settingName; }
        public Type ProviderType { get => _providerType; }
    }
}
