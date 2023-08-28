using DocumentCabinetLibrary;
using Microsoft.Extensions.Logging;

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
        var documentCabinet = InitializeDocumentCabinet();
        do
        {
            var command = ReadNumberOrCommand();

            if (command == exit) break;

            if (command == clean)
            {
                Console.Clear();
                continue;
            }

            Console.WriteLine("Found documents: ");
            foreach (var document in documentCabinet.FindDocumentsByNumberPart(command))
            {
                Console.WriteLine(document);
            }

        } while (true);
    }

    private static string ReadNumberOrCommand()
    {
        Console.WriteLine();
        Console.WriteLine("Type the document number or part of the number you want to search or '{0}' or '{1}':", exit, clean);
        return Console.ReadLine();
    }

    private static DocumentCabinet InitializeDocumentCabinet()
    {
        var cache = new DocumentCache();
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                   .AddFilter("System", LogLevel.Warning)
                   .AddConsole();
        });
        var storage = new FileDocumentStorage(cache, loggerFactory);
        var documentCabinet = new DocumentCabinet(storage);

        List<Document> documents = new List<Document>()
        {
            new Book()
            {
                Number = "SF-001",
                Title = "Test Sience Fiction Book",
                DatePublished = new DateTime(),
                Authors = new List<string> { "Test Author" },
                ISBN = "ISBN-001",
                NumberOfPages = 100,
                Publisher = "Test Publisher",
            },
            new LocalizedBook()
            {
                Number = "SF-002",
                Title = "Test Sience Fiction Book",
                DatePublished = new DateTime(),
                Authors = new List<string> { "Test Author" },
                ISBN = "ISBN-002",
                NumberOfPages = 100,
                OriginalPublisher = "Test Publisher 1",
                LocalPublisher = "Test Publisher 2",
                CountryOfLocalization = "Canada"
            },
            new Patient()
            {
                Number = "SE-001",
                Title = "Software Engineering Test Patient",
                DatePublished = new DateTime(),
                Authors = new List<string> { "Test Author" },
                ExpirationDate = new DateTime().AddDays(360),
                ID = "some_unique_id"
            },
            new Book()
            {
                Number = "DE-001",
                Title = "Test Detective Book",
                DatePublished = new DateTime(),
                Authors = new List<string> { "Test Author" },
                ISBN = "ISBN-003",
                NumberOfPages = 100,
                Publisher = "Test Publisher",
            },
            new Patient()
            {
                Number = "BE-001",
                Title = "Bio Engineering Test Patient",
                DatePublished = new DateTime(),
                Authors = new List<string> { "Test Author" },
                ExpirationDate = new DateTime().AddDays(240),
                ID = "some_unique_id2"
            },
            new Magazine()
            {
                Number = "AS-001",
                Title = "Astronomy Test Magazine",
                DatePublished = new DateTime(),
                Publisher = "Test Publisher",
                ReleaseNumber = 1,
            }
        };

        foreach (Document document in documents)
        {
            documentCabinet.AddDocument(document);
        }

        return documentCabinet;
    }
}