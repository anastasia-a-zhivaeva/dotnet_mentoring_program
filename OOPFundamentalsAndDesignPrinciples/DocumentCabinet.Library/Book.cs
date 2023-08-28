namespace DocumentCabinetLibrary
{
    [Serializable]
    public class Book : Document, IEquatable<Book>
    {
        public List<string> Authors { get; set; }
        public string ISBN { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }

        public Book(): base()
        {
            Authors = new List<string>();
            ISBN = string.Empty;
            NumberOfPages = 0;
            Publisher = string.Empty;
        }

        public Book(string number, string title, DateTime datePublished, List<string> authors, string iSBN, int numberOfPages, string publisher): base(number, title, datePublished)
        {
            Authors = authors;
            ISBN = iSBN;
            NumberOfPages = numberOfPages;
            Publisher = publisher;
        }

        public bool Equals(Book? other)
        {
            return base.Equals(other) && Authors.SequenceEqual(Authors) && ISBN == other.ISBN && NumberOfPages == other.NumberOfPages && Publisher == other.Publisher;
        }
    }
}