namespace DictionaryReplacer
{
    public class DictionaryReplacerTests
    {
        [Fact]
        public void Replace_ReturnsEmptyStringIfTemplateIsEmpty()
        {
            string template = "";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "temp", "temporary" } };

            string result = DictionaryProcessor.Replace(template, dictionary);

            Assert.Equal("", result);
        }

        [Fact]
        public void Replace_ThrowsErrorIfDictionaryIsNull()
        {
            string template = "";
            Dictionary<string, string> dictionary = null;

            string result = DictionaryProcessor.Replace(template, dictionary);

            Assert.Equal("", result);
        }

        [Fact]
        public void Replace_ReturnsEmptyStringIfDictionaryIsEmpty()
        {
            string template = "";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string result = DictionaryProcessor.Replace(template, dictionary);

            Assert.Equal("", result);
        }

        [Fact]
        public void Replace_ReturnsReplacedStringForOneValue()
        {
            string template = "$temp$";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "temp", "temporary" } };

            string result = DictionaryProcessor.Replace(template, dictionary);

            Assert.Equal("temporary", result);
        }
    }
}