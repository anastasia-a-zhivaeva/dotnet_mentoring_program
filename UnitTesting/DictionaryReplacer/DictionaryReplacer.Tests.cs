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

        [Fact]
        public void Replace_ReturnsReplacedStringForTwoAndMoreValues()
        {
            string template = "$temp$ here comes the name $name$ and number $number$";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "temp", "temporary" }, { "name", "John Doe" }, { "number", "1" } };

            string result = DictionaryProcessor.Replace(template, dictionary);

            Assert.Equal("temporary here comes the name John Doe and number 1", result);
        }

        [Fact]
        public void Replace_DoesNotReplaceStringIfNoValueInDictionary()
        {
            string template = "$temp$ here comes the name $name$ and number $number$";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "temp", "temporary" }, { "name", "John Doe" } };

            string result = DictionaryProcessor.Replace(template, dictionary);

            Assert.Equal("temporary here comes the name John Doe and number $number$", result);
        }

        [Fact]
        public void Replace_DoesNotReplaceStringIfExtraValueInDictionary()
        {
            string template = "$temp$ here comes the name $name$";
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "temp", "temporary" }, { "name", "John Doe" }, { "number", "1" } };

            string result = DictionaryProcessor.Replace(template, dictionary);

            Assert.Equal("temporary here comes the name John Doe", result);
        }
    }
}