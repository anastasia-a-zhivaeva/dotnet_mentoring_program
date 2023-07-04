namespace ConfigurationAttribute
{
    public class ProgramConfigurationComponent: ConfigurationComponentBase
    {
        [ConfigurationItem("int_test", ConfigurationProviderType.File)]
        public int IntTestFile { get; set; }

        [ConfigurationItem("int_test", ConfigurationProviderType.ConfigurationManager)]
        public int IntTestManager { get; set; }

        [ConfigurationItem("string_test", ConfigurationProviderType.File)]
        public string StringTestFile { get; set; }

        [ConfigurationItem("string_test", ConfigurationProviderType.ConfigurationManager)]
        public string StringTestManager { get; set; }

        [ConfigurationItem("float_test", ConfigurationProviderType.File)]
        public float FloatTestFile { get; set; }

        [ConfigurationItem("float_test", ConfigurationProviderType.ConfigurationManager)]
        public float FloatTestManager { get; set; }

        [ConfigurationItem("timespan_test", ConfigurationProviderType.File)]
        public TimeSpan TimeSpanTestFile { get; set; }

        [ConfigurationItem("timespan_test", ConfigurationProviderType.ConfigurationManager)]
        public TimeSpan TimeSpanTestManager { get; set; }

        public string ValueWithoutAttribute { get; set; }
    }
}
