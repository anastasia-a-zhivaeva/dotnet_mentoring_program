using System;

namespace HelloWorldLibrary
{
    public class Greeting
    {
        public static string Greet(string name = null)
        {
            var now = DateTime.Now.ToString();
            if (!string.IsNullOrWhiteSpace(name))
            {
                return $"{now} Hello, {name}!";

            }

            return $"{now} Hello, stranger!";
        }
    }
}
