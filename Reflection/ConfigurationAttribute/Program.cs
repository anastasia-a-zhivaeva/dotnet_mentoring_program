using ConfigurationAttribute;

var pconf = new ProgramConfigurationComponent();

pconf.LoadSettings();
Console.WriteLine("Load Settings");
DisplayValues(pconf);

Console.WriteLine("Generating random values...");
Console.WriteLine();
SetRandomValues(pconf);

pconf.SaveSettings();
Console.WriteLine("Save Settings");
DisplayValues(pconf);

pconf = new ProgramConfigurationComponent();
pconf.LoadSettings();
Console.WriteLine("Load Settings");
DisplayValues(pconf);

public static partial class Program
{
    public static void DisplayValues(ProgramConfigurationComponent pconf)
    {
        Console.WriteLine("FileConfigurationProvider values:");
        Console.WriteLine("int = {0}", pconf.IntTestFile);
        Console.WriteLine("float = {0}", pconf.FloatTestFile);
        Console.WriteLine("string = {0}", pconf.StringTestFile);
        Console.WriteLine("TimeSpan = {0}", pconf.TimeSpanTestFile);
        Console.WriteLine();

        Console.WriteLine("ConfigurationManagerConfigurationProvider values:");
        Console.WriteLine("int = {0}", pconf.IntTestManager);
        Console.WriteLine("float = {0}", pconf.FloatTestManager);
        Console.WriteLine("string = {0}", pconf.StringTestManager);
        Console.WriteLine("TimeSpan = {0}", pconf.TimeSpanTestManager);
        Console.WriteLine();

        Console.WriteLine("Value Without Attribute = {0}", pconf.ValueWithoutAttribute);
        Console.WriteLine();
    }

    public static void SetRandomValues(ProgramConfigurationComponent pconf)
    {
        var random = new Random();
        pconf.IntTestFile = random.Next();
        pconf.IntTestManager = random.Next();
        pconf.FloatTestFile = (float)(float.MaxValue * 2.0 * (random.NextDouble() - 0.5));
        pconf.FloatTestManager = (float)(float.MaxValue * 2.0 * (random.NextDouble() - 0.5));
        pconf.StringTestFile = Guid.NewGuid().ToString();
        pconf.StringTestManager = Guid.NewGuid().ToString();
        pconf.TimeSpanTestFile = DateTime.Now.TimeOfDay;
        pconf.TimeSpanTestManager = DateTime.Now.TimeOfDay;
        pconf.ValueWithoutAttribute = Guid.NewGuid().ToString();
    }
}