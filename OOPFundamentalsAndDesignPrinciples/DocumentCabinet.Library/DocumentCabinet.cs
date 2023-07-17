namespace DocumentCabinetLibrary
{
    public class DocumentCabinet
    {
        private IStorage<string, Document> _storage;

        public DocumentCabinet(IStorage<string, Document> storage) => _storage = storage;

        public void AddDocument(Document document) => _storage.Add(document);
        public IEnumerable<Document> FindDocumentsByNumberPart(string number) => _storage.FindByKeyPart(number);
        public IEnumerable<Document> GetAllDocuments() => _storage.GetAll();
        public Document GetDocument(string number) => _storage.Get(number);
        public void RemoveDocument(string number) => _storage.Remove(number);
    }
}
