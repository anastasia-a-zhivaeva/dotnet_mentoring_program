// See https://aka.ms/new-console-template for more information
if (args.Length > 0)
{
    foreach (var name in args) {
        Console.WriteLine($"Hello, {name}!");
     }
}
else
{
    Console.WriteLine("Hello, stranger!");
}

Console.ReadLine();