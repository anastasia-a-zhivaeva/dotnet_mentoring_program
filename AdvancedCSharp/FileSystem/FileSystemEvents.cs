namespace FileSystem
{
    public class FileSystemEnumerationStatusChangedEventArgs : EventArgs
    {
        public readonly string RootFolder;

        public FileSystemEnumerationStatusChangedEventArgs(string rootFolder) => RootFolder = rootFolder;
    }

    public class FileSystemEnumerationStartedEventArgs : FileSystemEnumerationStatusChangedEventArgs
    {
        public FileSystemEnumerationStartedEventArgs(string rootFolder) : base(rootFolder) { }
    }

    public class FileSystemEnumerationFinishedEventArgs : FileSystemEnumerationStatusChangedEventArgs
    {
        public FileSystemEnumerationFinishedEventArgs(string rootFolder) : base(rootFolder) { }
    }
    public class FileSystemEnumerationAbortedEventArgs : FileSystemEnumerationStatusChangedEventArgs
    {
        public FileSystemEnumerationAbortedEventArgs(string rootFolder) : base(rootFolder) { }
    }

    public class FileSystemInfoFoundEventArgs: EventArgs
    {
        public readonly string Path;

        public FileSystemInfoFoundEventArgs(string path) => Path = path;
    }

    public class FileSystemFileFoundEventArgs: FileSystemInfoFoundEventArgs
    {
        public FileSystemFileFoundEventArgs(string path): base(path) { }
    }

    public class FileSystemDirectoryFoundEventArgs: FileSystemInfoFoundEventArgs
    {
        public FileSystemDirectoryFoundEventArgs(string path): base(path) { }
    }

    public class FileSystemFilteredFileFoundEventArgs : FileSystemInfoFoundEventArgs
    {
        public FileSystemFilteredFileFoundEventArgs(string path) : base(path) { }
    }

    public class FileSystemFilteredDirectoryFoundEventArgs : FileSystemInfoFoundEventArgs
    {
        public FileSystemFilteredDirectoryFoundEventArgs(string path) : base(path) { }
    }
}