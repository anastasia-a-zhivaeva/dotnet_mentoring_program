using DocumentCabinetLibrary;

namespace DocumentCabinetTests
{
    public class DocumentCabinetTests: IDisposable
    {
        private DocumentCabinet _documentCabinet;
        private IStorage<string, Document> _storage;
        private List<Document> _documents = new List<Document>()
        {
            new Book()
            {
                Number = "SF-001",
                Title = "Test Sience Fiction Book",
                DatePublished = new DateTime(),
                Authors = new List<string> { "Test Author" },
                ISBN = "ISBN-001",
                NumberOfPages = 100,
                Publisher = "Test Publisher",
            },
            new LocalizedBook()
            {
                Number = "SF-002",
                Title = "Test Sience Fiction Book",
                DatePublished = new DateTime(),
                Authors = new List<string> { "Test Author" },
                ISBN = "ISBN-002",
                NumberOfPages = 100,
                OriginalPublisher = "Test Publisher 1",
                LocalPublisher = "Test Publisher 2",
                CountryOfLocalization = "Canada"
            },
            new Patient()
            {
                Number = "SE-001",
                Title = "Software Engineering Test Patient",
                DatePublished = new DateTime(),
                Authors = new List<string> { "Test Author" },
                ExpirationDate = new DateTime().AddDays(360),
                ID = "some_unique_id"
            },
            new Magazine()
            {
                Number = "AS-001",
                Title = "Astronomy Test Magazine",
                DatePublished = new DateTime(),
                Publisher = "Test Publisher",
                ReleaseNumber = 1,
            }
    }.OrderBy(x => x.Number).ToList();

        public DocumentCabinetTests() 
        {
            _storage = new FileStorage();
            _documentCabinet = new DocumentCabinet(_storage);
        }
        public void Dispose()
        {
           _storage.RemoveAll();
        }

        [Fact]
        public void AddDocumentTo_GetAllHasDocument()
        {            
            _documentCabinet.AddDocument(_documents[0]);

            Assert.Contains(_documents[0], _documentCabinet.GetAllDocuments());
        }

        [Fact]
        public void AddDocument_FindDocumentsByNumberPartHasDocument()
        {
            _documentCabinet.AddDocument(_documents[3]);

            Assert.Contains(_documents[3], _documentCabinet.FindDocumentsByNumberPart(_documents[3].Number));
        }

        [Fact]
        public void AddDocument_GetDocumentHasDocument()
        {
            _documentCabinet.AddDocument(_documents[1]);

            Assert.Equal(_documents[1], _documentCabinet.GetDocument(_documents[1].Number));
        }

        [Fact]
        public void GetAllDocuments_ReturnsAllAddedDocuments()
        {
            AddAllDocumentsToStorage();

            var allDocuments = _documentCabinet.GetAllDocuments();

            Assert.True(Enumerable.SequenceEqual(_documents, allDocuments.OrderBy(x => x.Number)));
        }

        [Fact]
        public void FindDocumentsByNumberPart_ReturnsDocumentsThatContainPartOfNumber()
        {
            AddAllDocumentsToStorage();

            var foundDocument = _documentCabinet.FindDocumentsByNumberPart("SF");

            Assert.True(Enumerable.SequenceEqual(_documents.FindAll(x => x.Number.Contains("SF")),foundDocument.OrderBy(x => x.Number)));
        }

        [Fact]
        public void RemoveDocument_GetAllDocumentsDoesNotHaveDocument()
        {
            AddAllDocumentsToStorage();

            _documentCabinet.RemoveDocument(_documents[0].Number);

            Assert.DoesNotContain(_documents[0], _documentCabinet.GetAllDocuments());
        }

        [Fact]
        public void RemoveDocument_FindDocumentsByNumberPartDoesNotHaveDocument()
        {
            AddAllDocumentsToStorage();

            _documentCabinet.RemoveDocument(_documents[0].Number);

            Assert.DoesNotContain(_documents[0], _documentCabinet.FindDocumentsByNumberPart(_documents[0].Number));
        }

        [Fact]
        public void RemoveDocument_GetDocumentThrowsError()
        {
            AddAllDocumentsToStorage();

            _documentCabinet.RemoveDocument(_documents[0].Number);

            Assert.Throws<KeyNotFoundException>(() => _documentCabinet.GetDocument(_documents[0].Number));
        }

        [Fact]
        public void RemoveDocument_ThrowsErrorIfDocumentDoesNotExist()
        {
           Assert.Throws<KeyNotFoundException>(() => _documentCabinet.RemoveDocument(_documents[0].Number));
        }

        private void AddAllDocumentsToStorage()
        {
            foreach (Document document in _documents)
            {
                _documentCabinet.AddDocument(document);
            }
        }
    }
}