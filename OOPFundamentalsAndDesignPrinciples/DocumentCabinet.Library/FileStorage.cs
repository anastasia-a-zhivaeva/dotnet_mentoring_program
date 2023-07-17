using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace DocumentCabinetLibrary
{
    public class FileStorage: IStorage<string, Document>
    {
        private readonly string _path = new StringBuilder(Directory.GetCurrentDirectory()).Append(@"\DocumentCabinet").ToString();
        private DirectoryInfo _directory;

        public FileStorage()
        {
            CreateDirectory();
        }

        public void Add(Document value)
        {
            var filePath = Path.Combine(_path, $"{value.GetType().Name}_{value.Number}.json");
            using (var writer = File.Create(filePath))
            {
                var type = value.GetType();
                JsonSerializer.Serialize(writer, value);
                writer.Dispose();
            }
        }

        public Document Get(string key)
        {
            foreach (var item in _directory.EnumerateFiles())
            {
                var foundKey = ExtractKey(item.Name);
                if (foundKey == key)
                {
                    using (var reader = File.OpenRead(item.FullName))
                    {
                        Document document = JsonSerializer.Deserialize<Document>(reader);
                        reader.Dispose();
                        return document;
                    }
                }
            }

            throw new KeyNotFoundException();
        }

        public IEnumerable<Document> GetAll()
        {
            foreach (var item in _directory.EnumerateFiles())
            {
                using (var reader = File.OpenRead(item.FullName))
                {
                    Document document = JsonSerializer.Deserialize<Document>(reader);
                    reader.Dispose();
                    yield return document;
                }
            }
        }

        public IEnumerable<Document> FindByKeyPart(string key)
        {
            foreach (var item in _directory.EnumerateFiles())
            {
                var foundKey = ExtractKey(item.Name);
                if (foundKey.Contains(key))
                {
                    using (var reader = File.OpenRead(item.FullName))
                    {
                        Document document = JsonSerializer.Deserialize<Document>(reader);
                        reader.Dispose();
                        yield return document;
                    }
                }
            }
        }

        public void Remove(string key)
        {
            foreach (var item in _directory.EnumerateFiles())
            {
                var foundKey = ExtractKey(item.Name);
                if (foundKey == key)
                {
                    item.Delete();
                    return;
                }
            }

            throw new KeyNotFoundException();
        }

        public void RemoveAll()
        {
            foreach(var item in _directory.EnumerateFiles())
            {
                item.Delete();
            }
        }

        private void CreateDirectory()
        {
            _directory = Directory.CreateDirectory(_path);
        }

        private string ExtractKey(string name)
        {
            return name.Split('_').Last().Split('.').First();
        }
    }
}