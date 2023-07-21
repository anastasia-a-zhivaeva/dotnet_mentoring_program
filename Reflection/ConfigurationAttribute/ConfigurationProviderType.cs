using System;
using System.ComponentModel;
namespace ConfigurationAttribute
{
    public enum ConfigurationProviderType
    {
        [Description("FileConfigurationProvider")]
        File,
        [Description("ConfigurationManagerConfigurationProvider")]
        ConfigurationManager,
    }
}
