using HelloWorldLibrary;

// See https://aka.ms/new-console-template for more information
if (args.Length > 0)
{
    foreach (var name in args) {
        Console.WriteLine(Greeting.Greet(name));
     }
}
else
{
    Console.WriteLine(Greeting.Greet());
}

Console.ReadLine();