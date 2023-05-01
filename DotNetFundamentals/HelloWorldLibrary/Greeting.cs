using System;
using System.Text;
using System.Linq;

namespace HelloWorldLibrary
{
    public class Greeting
    {
        public string Greet(string[] names = null)
        {
            var now = DateTime.Now.ToString();
            StringBuilder stringBuilder = new StringBuilder();

            if (names == null || names.Length == 0)
            {
                return stringBuilder.AppendFormat("{0} Hello, stranger!", now).ToString();
            }

            foreach (var name in names)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    stringBuilder.AppendFormat("{0} Hello, {1}!", now, name.Trim()).AppendLine();

                }
            }
            return stringBuilder.ToString();
        }

        public string[] ParseNames(string names)
        {
            if (names == null)
            {
                return null;
            }

            return names.Split(',').Where(s => s.Length > 0).ToArray();
        }
    }
}
