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
    }
}