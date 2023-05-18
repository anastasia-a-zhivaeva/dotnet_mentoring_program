using System;

namespace Task1
{
    internal class Program
    {
        private static readonly string exit = "exit";
        private static readonly string clean = "clean";

        private static void Main(string[] args)
        {
            StartMainLoop();
        }

        private static void StartMainLoop()
        {
            do 
            {
                var inputLine = ReadInputLineOrCommand();

                if (inputLine == exit) break;

                if (inputLine == clean)
                {
                    Console.Clear();
                    continue;
                }

                WriteFirstCharacter(inputLine);
            } while (true);
        }

        private static string ReadInputLineOrCommand()
        {
            Console.WriteLine();
            Console.WriteLine("Type something to extract first character or '{0}' or '{1}':", exit, clean);
            return Console.ReadLine();
        }

        private static void WriteFirstCharacter(string inputLine)
        {
            try
            {
                Console.WriteLine(inputLine[0]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Input line cannot be empty!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}