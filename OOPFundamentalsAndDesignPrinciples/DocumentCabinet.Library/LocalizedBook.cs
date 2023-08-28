namespace DocumentCabinetLibrary
{
    [Serializable]
    public class LocalizedBook : Document, IEquatable<LocalizedBook>
    {
        public List<string> Authors { get; set; }
        public string ISBN { get; set; }
        public int NumberOfPages { get; set; }
        public string OriginalPublisher { get; set; }
        public string LocalPublisher { get; set; }
        public string CountryOfLocalization { get; set; }

        public LocalizedBook(): base()
        {
            Authors = new List<string>();
            ISBN = string.Empty;
            NumberOfPages = 0;
            OriginalPublisher = string.Empty;
            LocalPublisher = string.Empty;
            CountryOfLocalization = string.Empty;
        }

        public LocalizedBook(string number, string title, DateTime datePublished, List<string> authors, string iSBN, int numberOfPages, string originalPublisher, string localPublisher, string countryOfLocalization): base(number, title, datePublished)
        {
            Authors = authors;
            ISBN = iSBN;
            NumberOfPages = numberOfPages;
            OriginalPublisher = originalPublisher;
            LocalPublisher = localPublisher;
            CountryOfLocalization = countryOfLocalization;
        }
        public bool Equals(LocalizedBook? other)
        {
            return base.Equals(other) && Authors.SequenceEqual(Authors) && ISBN == other.ISBN && NumberOfPages == other.NumberOfPages && OriginalPublisher == other.OriginalPublisher && LocalPublisher == other.LocalPublisher && CountryOfLocalization == other.CountryOfLocalization;
        }
    }
}