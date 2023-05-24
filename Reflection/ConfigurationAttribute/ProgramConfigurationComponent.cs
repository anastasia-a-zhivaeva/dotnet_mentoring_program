namespace ConfigurationAttribute
{
    public class ProgramConfigurationComponent: ConfigurationComponentBase
    {
        [ConfigurationItem("int_test", typeof(FileConfigurationProvider))]
        public int IntTestFile { get; set; }

        [ConfigurationItem("int_test", typeof(ConfigurationManagerConfigurationProvider))]
        public int IntTestManager { get; set; }

        [ConfigurationItem("string_test", typeof(FileConfigurationProvider))]
        public string StringTestFile { get; set; }

        [ConfigurationItem("string_test", typeof(ConfigurationManagerConfigurationProvider))]
        public string StringTestManager { get; set; }

        [ConfigurationItem("float_test", typeof(FileConfigurationProvider))]
        public float FloatTestFile { get; set; }

        [ConfigurationItem("float_test", typeof(ConfigurationManagerConfigurationProvider))]
        public float FloatTestManager { get; set; }

        [ConfigurationItem("timespan_test", typeof(FileConfigurationProvider))]
        public TimeSpan TimeSpanTestFile { get; set; }

        [ConfigurationItem("timespan_test", typeof(ConfigurationManagerConfigurationProvider))]
        public TimeSpan TimeSpanTestManager { get; set; }
    }
}
