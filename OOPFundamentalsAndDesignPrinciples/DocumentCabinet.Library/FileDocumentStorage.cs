using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace DocumentCabinetLibrary
{
    public class FileDocumentStorage: IStorage<string, Document>
    {
        private readonly string _path = new StringBuilder(Directory.GetCurrentDirectory()).Append(@"\DocumentCabinet").ToString();
        private ICache<string,Document> _cache;
        private ILogger<FileDocumentStorage> _logger;
        private DirectoryInfo _directory;

        public FileDocumentStorage(ICache<string, Document> cache, ILoggerFactory loggerFactory)
        {
            _cache = cache;
            _logger = loggerFactory.CreateLogger<FileDocumentStorage>();
            _directory = Directory.CreateDirectory(_path);
        }

        public void Add(Document value)
        {
            var filePath = Path.Combine(_path, $"{value.GetType().Name}_{value.Number}.json");
            using (var writer = File.Create(filePath))
            {
                _logger.LogInformation($"Adding {value.Number} to storage, no caching");
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
                        Document document = _cache.Get(foundKey);
                        if (document == null)
                        {
                            _logger.LogInformation($"Not a cache hit for {foundKey}");
                            document = JsonSerializer.Deserialize<Document>(reader);
                            _cache.Set(foundKey, document);
                        }
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
                    var key = ExtractKey(item.Name);
                    Document document = _cache.Get(key);
                    if (document == null)
                    {
                        _logger.LogInformation($"Not a cache hit for {key}");
                        document = JsonSerializer.Deserialize<Document>(reader);
                        _cache.Set(key, document);
                    }
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
                        Document document = _cache.Get(foundKey);
                        if (document == null)
                        {
                            _logger.LogInformation($"Not a cache hit for {foundKey}");
                            document = JsonSerializer.Deserialize<Document>(reader);
                            _cache.Set(foundKey, document);
                        }
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
                    _cache.Remove(foundKey);
                    return;
                }
            }

            throw new KeyNotFoundException();
        }

        public void RemoveAll()
        {
            foreach(var item in _directory.EnumerateFiles())
            {
                var key = ExtractKey(item.Name);
                item.Delete();
                _cache.Remove(key);
            }
        }

        private string ExtractKey(string name)
        {
            return name.Split('_').Last().Split('.').First();
        }
    }
}