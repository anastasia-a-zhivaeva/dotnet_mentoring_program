using System.Text;
using ConfigurationProviderBaseLibrary;

namespace ConfigurationAttribute
{
    public class FileConfigurationProvider: IConfigurationProvider
    {
        private readonly string _settingsFile = new StringBuilder(Directory.GetCurrentDirectory()).Append(@"\settings.txt").ToString();
        private readonly char _delimiter = '=';

        public string? GetSetting(string settingName)
        {
            string? setting;
            using (var fs = File.Open(_settingsFile, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    while ((setting = sr.ReadLine()) is not null)
                    {
                        var settingPair = setting.Split(_delimiter);
                        if (settingPair[0] == settingName)
                        {
                            return settingPair[1];
                        }
                    }
                }
            }
            return null;
        }

        public void SetSetting(string settingName, string? value)
        {
            string? setting;
            string settings = "";
            bool rewrite = false;
            using (var fs = File.Open(_settingsFile, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    while ((setting = sr.ReadLine()) is not null)
                    {
                        var settingPair = setting.Split(_delimiter);
                        if (settingPair[0] == settingName)
                        {
                            settingPair[1] = value;
                            setting = string.Join(_delimiter, settingPair);
                            rewrite = true;
                        }
                        settings += new StringBuilder(setting).AppendLine();
                    }
                    if (!rewrite)
                    {
                        settings += new StringBuilder(settingName).Append(_delimiter).AppendLine(value);
                    }
                }
            }
            using (var fs = File.Open(_settingsFile, FileMode.Truncate, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(settings);
                }
            }
        }
    }
}
