using System;

namespace FileSystem
{
    public delegate bool FileSystemInfoFilter(FileSystemInfo info);

    public class FileSystemVisitor
    {
        private readonly DirectoryInfo _root;
        private readonly FileSystemInfoFilter? _filter;
        private readonly bool? _abort = false;
        private readonly bool? _exclude = true;

        public event EventHandler<FileSystemEnumerationStartedEventArgs> FileSystemEnumerationStarted;
        public event EventHandler<FileSystemEnumerationFinishedEventArgs> FileSystemEnumerationFinished;
        public event EventHandler<FileSystemEnumerationAbortedEventArgs> FileSystemEnumerationAborted;
        public event EventHandler<FileSystemFileFoundEventArgs> FileSystemFileFound;
        public event EventHandler<FileSystemDirectoryFoundEventArgs> FileSystemDirectoryFound;
        public event EventHandler<FileSystemFilteredFileFoundEventArgs> FileSystemFilteredFileFound;
        public event EventHandler<FileSystemFilteredDirectoryFoundEventArgs> FileSystemFilteredDirectoryFound;

        public FileSystemVisitor(string rootFolder)
        {
            if (string.IsNullOrEmpty(rootFolder))
            {
                throw new ArgumentNullException(nameof(rootFolder), "Root folder cannot be empty");
            }

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

        public FileSystemVisitor(string rootFolder, FileSystemInfoFilter? filter, bool? abort, bool? exclude) : this(rootFolder)
        {
            _filter = filter;
            _abort = abort;
            _exclude = exclude;
        }

        public IEnumerable<FileSystemInfo> EnumerateFileSystem()
        {
            OnFileSystemEnumerationStarted(new FileSystemEnumerationStartedEventArgs(_root.FullName));
            foreach(var info in EnumerateDirectory(_root))
            {
                yield return info;
            }
            OnFileSystemEnumerationFinished(new FileSystemEnumerationFinishedEventArgs(_root.FullName));
        }

        private IEnumerable<FileSystemInfo> EnumerateDirectory(DirectoryInfo directory)
        {
            foreach (var info in directory.EnumerateFileSystemInfos())
            {
                RaiseFileSystemInfoFoundEvent(info);

                if (_filter != null && _filter(info))
                {
                    RaiseFileSystemFilteredInfoFoundEvent(info);

                    if (_exclude == false)
                    {
                        yield return info;
                    }

                    if (_abort == true)
                    {
                        OnFileSystemEnumerationAborted(new FileSystemEnumerationAbortedEventArgs(_root.FullName));
                        yield break;
                    }
                } 
                else
                {
                    yield return info;
                }

                if (info.IsDirectory())
                {
                    foreach (var subInfo in EnumerateDirectory((DirectoryInfo)info))
                    {
                         yield return subInfo;
                    }
                }
            }
        }

        protected virtual void OnFileSystemEnumerationStarted(FileSystemEnumerationStartedEventArgs e)
        {
            EventHandler<FileSystemEnumerationStartedEventArgs> eventHandler = FileSystemEnumerationStarted;
            RaiseEvent(eventHandler, e);
        }

        protected virtual void OnFileSystemEnumerationFinished(FileSystemEnumerationFinishedEventArgs e)
        {
            EventHandler<FileSystemEnumerationFinishedEventArgs> eventHandler = FileSystemEnumerationFinished;  
            RaiseEvent(eventHandler, e);
        }

        protected virtual void OnFileSystemEnumerationAborted(FileSystemEnumerationAbortedEventArgs e)
        {
            EventHandler<FileSystemEnumerationAbortedEventArgs> eventHandler = FileSystemEnumerationAborted;
            RaiseEvent(eventHandler, e);
        }

        protected virtual void OnFileSystemFileFound(FileSystemFileFoundEventArgs e) 
        { 
            EventHandler<FileSystemFileFoundEventArgs> eventHandler = FileSystemFileFound;
            RaiseEvent(eventHandler, e);
        }

        protected virtual void OnFileSystemDirectoryFound(FileSystemDirectoryFoundEventArgs e)
        {
            EventHandler<FileSystemDirectoryFoundEventArgs> eventHandler = FileSystemDirectoryFound;
            RaiseEvent(eventHandler, e);
        }

        protected virtual void OnFileSystemFilteredFileFound(FileSystemFilteredFileFoundEventArgs e)
        {
            EventHandler<FileSystemFilteredFileFoundEventArgs> eventHandler = FileSystemFilteredFileFound;
            RaiseEvent(eventHandler, e);
        }

        protected virtual void OnFileSystemFilteredDirectoryFound(FileSystemFilteredDirectoryFoundEventArgs e)
        {
            EventHandler<FileSystemFilteredDirectoryFoundEventArgs> eventHandler = FileSystemFilteredDirectoryFound;
            RaiseEvent(eventHandler, e);
        }

        private void RaiseEvent<T>(EventHandler<T> eventHandler, T e)
        {
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        private void RaiseFileSystemInfoFoundEvent(FileSystemInfo info)
        {
            if (info.IsFile())
                OnFileSystemFileFound(new FileSystemFileFoundEventArgs(info.FullName));

            if (info.IsDirectory())
                OnFileSystemDirectoryFound(new FileSystemDirectoryFoundEventArgs(info.FullName));
        }

        private void RaiseFileSystemFilteredInfoFoundEvent(FileSystemInfo info)
        {
            if (info.IsFile())
                OnFileSystemFilteredFileFound(new FileSystemFilteredFileFoundEventArgs(info.FullName));

            if (info.IsDirectory())
                OnFileSystemFilteredDirectoryFound(new FileSystemFilteredDirectoryFoundEventArgs(info.FullName));
        }
    }
}
