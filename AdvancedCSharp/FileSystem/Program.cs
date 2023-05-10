using FileSystem;

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
            var folderPath = ReadFolderPathOrCommand();

            if (folderPath == null) continue;

            if (folderPath == exit) break;

            if (folderPath == clean)
            {
                Console.Clear();
                continue;
            }

            var fileSystemFilter = new FileSystemFilter();
            fileSystemFilter.ReadFilterChoice();

            try
            {
                DisplayFilesAndDirectories(folderPath, fileSystemFilter.GetFileSystemInfoFilter());
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException)
                {
                    Console.WriteLine(e.Message);
                }
                else
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        } while (true);
    }

    private static string? ReadFolderPathOrCommand()
    {
        Console.WriteLine("Type a folder path or '{0}' or '{1}':", exit, clean);
        return Console.ReadLine();
    }

    private static void DisplayFilesAndDirectories(string folderPath, FileSystemInfoFilter? filter)
    {
        var fileSystemVisitor = new FileSystemVisitor(folderPath, filter);
        var fileSystem = fileSystemVisitor.EnumerateFileSystem();
        foreach (var info in fileSystem)
        {
            Console.WriteLine("{0} - {1}", info.GetFileSystemInfoType(), info.FullName);
        }
    }
}