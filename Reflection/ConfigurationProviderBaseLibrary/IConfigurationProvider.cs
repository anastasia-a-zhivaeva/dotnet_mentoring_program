namespace ConfigurationProviderBaseLibrary
{
    public interface IConfigurationProvider
    {
        string? GetSetting(string settingName);
        void SetSetting(string settingName, string? value);
    }
}