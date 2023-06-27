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
                DisplayFilesAndDirectories(folderPath, fileSystemFilter.Filter, fileSystemFilter.AbortOption, fileSystemFilter.ExcludeOption);
            }
            catch (Exception e) when (e is DirectoryNotFoundException || e is ArgumentNullException)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                break;
            }
        } while (true);
    }

    private static string? ReadFolderPathOrCommand()
    {
        Console.WriteLine();
        Console.WriteLine("Type a folder path or '{0}' or '{1}':", exit, clean);
        return Console.ReadLine();
    }

    private static void DisplayFilesAndDirectories(string folderPath, FileSystemInfoFilter? filter, bool abort, bool exclude)
    {
        Console.WriteLine();
        var fileSystemVisitor = new FileSystemVisitor(folderPath, filter, abort, exclude);
        fileSystemVisitor.FileSystemEnumerationStarted += (sender, e) => Console.WriteLine("Enumeration of {0} is started by {1}", e.RootFolder, sender);
        fileSystemVisitor.FileSystemEnumerationFinished += (sender, e) => Console.WriteLine("Enumeration of {0} is finished by {1}", e.RootFolder, sender);
        fileSystemVisitor.FileSystemEnumerationAborted += (sender, e) => Console.WriteLine("Enumeration of {0} is aborted by {1}", e.RootFolder, sender);
        fileSystemVisitor.FileSystemFileFound += (sender, e) => Console.WriteLine("File found - {0} by {1}", e.Path, sender);
        fileSystemVisitor.FileSystemDirectoryFound += (sender, e) => Console.WriteLine("Directory found - {0} by {1}", e.Path, sender);
        fileSystemVisitor.FileSystemFilteredFileFound += (sender, e) => Console.WriteLine("Filtered file found - {0} by {1}", e.Path, sender);
        fileSystemVisitor.FileSystemFilteredDirectoryFound += (sender, e) => Console.WriteLine("Filtered directory found - {0} by {1}", e.Path, sender);
        var fileSystem = fileSystemVisitor.EnumerateFileSystem();
        foreach (var info in fileSystem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0} Result - {1}", info.GetFileSystemInfoType(), info.FullName);
            Console.ResetColor();
        }
    }
}