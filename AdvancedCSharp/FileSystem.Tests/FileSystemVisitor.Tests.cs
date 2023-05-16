using System.Text;

namespace FileSystem.Tests
{
    public class FileSystemVisitorTests
    {
        private string _testFolder = new StringBuilder(Directory.GetCurrentDirectory()).Append(@"\TestFolder").ToString();

        [Fact]
        public void FileSystemVisitor_CreatedSuccessfullyIfRootFolderIsValid()
        {
            Assert.NotNull(new FileSystemVisitor(_testFolder));
        }

        [Fact]
        public void FileSystemVisitor_CreatedUnsuccessfullyIfRootFolderIsInvalid()
        {
            Assert.Throws<DirectoryNotFoundException>(() => new FileSystemVisitor("NotExistingFolder"));
        }

        [Fact]
        public void FileSystemVisitor_CreatedSuccessfullyIfRootFolderIsValidAndFilterIsNull()
        {
            Assert.NotNull(new FileSystemVisitor(_testFolder, null));
        }

        [Fact]
        public void FileSystemVisitor_CreatedSuccessfullyIfRootFolderIsValidAndFilterIsProvided()
        {
            Assert.NotNull(new FileSystemVisitor(_testFolder, (_) => true));
        }

        [Fact]
        public void EnumerateFileSystem_ReturnsAllFilesAndDirectoriesIfFilterIsNotProvided()
        {
            var fileSystemVisitor = new FileSystemVisitor(_testFolder);
            var fileSystemInfos = fileSystemVisitor.EnumerateFileSystem();
            
            Assert.NotEmpty(fileSystemInfos);
            Assert.Equal(9, fileSystemInfos.Count());
        }

        [Fact]
        public void EnumerateFileSystem_FiltersAllDirectoriesIfFilterIsProvidedForDirectories()
        {
            var fileSystemVisitor = new FileSystemVisitor(_testFolder, (info) => info.IsDirectory());
            var fileSystemInfos = fileSystemVisitor.EnumerateFileSystem();

            Assert.NotEmpty(fileSystemInfos);
            Assert.Equal(5, fileSystemInfos.Count());
            Assert.All(fileSystemInfos, (info) => info.IsFile());
        }

        [Fact]
        public void EnumerateFileSystem_FiltersAllFilesIfFilterIsProvidedForFiles()
        {
            var fileSystemVisitor = new FileSystemVisitor(_testFolder, (info) => info.IsFile());
            var fileSystemInfos = fileSystemVisitor.EnumerateFileSystem();

            Assert.NotEmpty(fileSystemInfos);
            Assert.Equal(4, fileSystemInfos.Count());
            Assert.All(fileSystemInfos, (info) => info.IsDirectory());
        }

        [Fact]
        public void EnumerateFileSystem_FiltersAllFoundFilesAndDirectoriesIfFilterIsProvidedForContains()
        {
            var fileSystemVisitor = new FileSystemVisitor(_testFolder, (info) => info.Contains("keyword"));
            var fileSystemInfos = fileSystemVisitor.EnumerateFileSystem();

            Assert.NotEmpty(fileSystemInfos);
            Assert.Equal(6, fileSystemInfos.Count());
            Assert.All(fileSystemInfos, (info) => Assert.DoesNotContain(info.Name, "keyword"));
        }
    }
}