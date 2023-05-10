namespace FileSystem
{
    public delegate bool FileSystemInfoFilter(FileSystemInfo info);

    public class FileSystemVisitor
    {
        private readonly DirectoryInfo _root;
        private readonly FileSystemInfoFilter? _filter;

        public string RootFolder => _root.FullName;

        public FileSystemVisitor(string rootFolder)
        {
            _root = new DirectoryInfo(rootFolder);
            if (!_root.Exists)
            {
                throw new DirectoryNotFoundException();
            }
        }

        public FileSystemVisitor(string rootFolder, FileSystemInfoFilter? filter) : this(rootFolder)
        {
            _filter = filter;
        }

        public IEnumerable<FileSystemInfo> EnumerateFileSystem()
        {
            return EnumerateDirectory(_root);
        }

        private IEnumerable<FileSystemInfo> EnumerateDirectory(DirectoryInfo directory)
        {
            foreach (var info in directory.EnumerateFileSystemInfos())
            {
                if (_filter == null || _filter(info))
                    yield return info;

                if (info.IsDirectory())
                {
                    foreach (var subInfo in EnumerateDirectory((DirectoryInfo)info))
                    {
                        if (_filter == null || _filter(subInfo))
                            yield return subInfo;
                    }
                }
            }
        }
    }
}
