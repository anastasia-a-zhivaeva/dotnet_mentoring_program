namespace FileSystem
{
    public enum FileSystemInfoType
    {
        File,
        Directory,
    }

    public static class FileSystemInfoHelper
    {
        public static FileSystemInfoType GetFileSystemInfoType(this FileSystemInfo info) => info is DirectoryInfo ? FileSystemInfoType.Directory : FileSystemInfoType.File;

        public static bool IsFile(this FileSystemInfo info) => info.GetFileSystemInfoType() == FileSystemInfoType.File;

        public static bool IsDirectory(this FileSystemInfo info) => info.GetFileSystemInfoType() == FileSystemInfoType.Directory;

        public static bool Contains(this FileSystemInfo info, string value) => info.Name.ToLower().Contains(value.ToLower());
    }
}
