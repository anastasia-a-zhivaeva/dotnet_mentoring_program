namespace DictionaryReplacer
{
    internal class DictionaryProcessor
    {
        internal static string Replace(string template, Dictionary<string, string> dictionary)
        {
            if (string.IsNullOrEmpty(template.Trim()) || dictionary.Count == 0)
            {
                return "";
            }

            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            string result = template;

            foreach (var replacement in dictionary)
            {
                result = result.Replace($"${replacement.Key}$", replacement.Value);
            }

            return result;
        }
    }
}