namespace FileSystem
{
    public enum FileSystemFilterOption
    {
        NO_FILTER = 1,
        IS_FILE,
        IS_DIRECTORY,
        CONTAINS,
    }

    public class FileSystemFilter
    {
        private FileSystemFilterOption _option = FileSystemFilterOption.NO_FILTER;

        private Dictionary<FileSystemFilterOption, FileSystemInfoFilter?> _filters = new Dictionary<FileSystemFilterOption, FileSystemInfoFilter?>()
        {
            { FileSystemFilterOption.NO_FILTER, null },
            { FileSystemFilterOption.IS_FILE, (info) => info.IsFile() },
            { FileSystemFilterOption.IS_DIRECTORY, (info) => info.IsDirectory() },
        };

        private Dictionary<FileSystemFilterOption, string> _filterDisplayNames = new Dictionary<FileSystemFilterOption, string>()
        {
            { FileSystemFilterOption.NO_FILTER, "No filter (default)" },
            { FileSystemFilterOption.IS_FILE, "Display files only" },
            { FileSystemFilterOption.IS_DIRECTORY, "Display directories only" },
            { FileSystemFilterOption.CONTAINS, "Display files and directories with name containing a string" }
        };

        public void ReadFilterChoice()
        {
            foreach (KeyValuePair<FileSystemFilterOption, string> filterDisplayName in _filterDisplayNames)
            {
                Console.WriteLine("{0}. {1}", (int)filterDisplayName.Key, filterDisplayName.Value);
            }

            try
            {
                _option = (FileSystemFilterOption)int.Parse(Console.ReadLine() ?? "1");
            }
            catch
            {
                Console.WriteLine("Wrong value was provided, no filter (default) is chosen");
                _option = FileSystemFilterOption.NO_FILTER;
            }

            if (_option == FileSystemFilterOption.CONTAINS)
                SetContainsFilterValue();
        }

        public FileSystemInfoFilter? GetFileSystemInfoFilter()
        {
            return _filters.GetValueOrDefault(_option);
        }

        private void SetContainsFilterValue()
        {
            Console.WriteLine("Type value to filter names:");
            var containsFilterValue = Console.ReadLine() ?? "";

            FileSystemInfoFilter? result;
            FileSystemInfoFilter containsFilter = (info) => info.Contains(containsFilterValue);
            if (_filters.TryGetValue(FileSystemFilterOption.CONTAINS, out result))
            {
                _filters[FileSystemFilterOption.CONTAINS] = containsFilter;
            }
            else
            {
                _filters.Add(FileSystemFilterOption.CONTAINS, containsFilter);
            }
        }
    }
}
