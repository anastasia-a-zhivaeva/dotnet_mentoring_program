using System.ComponentModel;
using System.Reflection;

namespace ConfigurationAttribute
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString<T>(this T value) where T : Enum
        {
            var field = typeof(T).GetField(value.ToString());
            DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? string.Empty;
        }

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute?.Description == description)
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Named constant with specified description is not found", nameof(description));
        }
    }
}
