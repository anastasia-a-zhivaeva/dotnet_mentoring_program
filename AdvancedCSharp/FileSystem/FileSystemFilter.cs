namespace FileSystem
{
    public enum FileSystemFilterOption
    {
        NO_FILTER = 1,
        IS_FILE,
        IS_DIRECTORY,
        CONTAINS,
    }

    public enum FileSystemFilterAbortOption
    {
        NO = 1,
        YES,
    }

    public enum FileSystemFilterExcludeOption
    {
        YES = 1,
        NO,
    }

    public class FileSystemFilter
    {
        private FileSystemFilterOption _option = FileSystemFilterOption.NO_FILTER;
        private FileSystemFilterAbortOption _abort = FileSystemFilterAbortOption.NO;
        private FileSystemFilterExcludeOption _exclude = FileSystemFilterExcludeOption.YES;

        private Dictionary<FileSystemFilterOption, FileSystemInfoFilter?> _filters = new Dictionary<FileSystemFilterOption, FileSystemInfoFilter?>()
        {
            { FileSystemFilterOption.NO_FILTER, null },
            { FileSystemFilterOption.IS_FILE, (info) => info.IsFile() },
            { FileSystemFilterOption.IS_DIRECTORY, (info) => info.IsDirectory() },
        };

        private Dictionary<FileSystemFilterOption, string> _filterDisplayNames = new Dictionary<FileSystemFilterOption, string>()
        {
            { FileSystemFilterOption.NO_FILTER, "No filter (default)" },
            { FileSystemFilterOption.IS_FILE, "Filter files" },
            { FileSystemFilterOption.IS_DIRECTORY, "Filter directories" },
            { FileSystemFilterOption.CONTAINS, "Filter files and directories with name containing a string" }
        };

        private Dictionary<FileSystemFilterAbortOption, bool> _abortOptions = new Dictionary<FileSystemFilterAbortOption, bool>()
        {
            { FileSystemFilterAbortOption.NO, false },
            { FileSystemFilterAbortOption.YES, true },
        };

        private Dictionary<FileSystemFilterExcludeOption, bool> _excludeOptions = new Dictionary<FileSystemFilterExcludeOption, bool>()
        {
            { FileSystemFilterExcludeOption.NO, false },
            { FileSystemFilterExcludeOption.YES, true },
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

            if (_option != FileSystemFilterOption.NO_FILTER)
            {
                SetAbortChoiceValue();
                SetExcludeChoiceValue();
            }
        }

        public FileSystemInfoFilter? Filter
        {
            get => _filters.GetValueOrDefault(_option);
        }

        public bool AbortOption
        {
            get => _abortOptions.GetValueOrDefault(_abort);
        }

        public bool ExcludeOption
        {
            get => _excludeOptions.GetValueOrDefault(_exclude);
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

        private void SetAbortChoiceValue()
        {
            Console.WriteLine(
                """
                Would you like to abort enumeration when first filtered file/directory found?
                1. No (default)
                2. Yes
                """
            );

            try
            {
                _abort = (FileSystemFilterAbortOption)int.Parse(Console.ReadLine() ?? "1");
            }
            catch
            {
                _abort = FileSystemFilterAbortOption.NO;
            }
        }

        private void SetExcludeChoiceValue()
        {
            Console.WriteLine(
                """
                Would you like to exclude found filtered file/directory from the final result?
                1. Yes (default)
                2. No
                """
            );

            try
            {
                _exclude = (FileSystemFilterExcludeOption)int.Parse(Console.ReadLine() ?? "1");
            }
            catch
            {
                _exclude = FileSystemFilterExcludeOption.YES;
            }
        }
    }
}
