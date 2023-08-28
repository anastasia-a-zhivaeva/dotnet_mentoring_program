namespace DocumentCabinetLibrary
{
    [Serializable]
    public class Magazine : Document, IEquatable<Magazine>
    {
        public string Publisher { get; set; }
        public int ReleaseNumber { get; set; }

        public Magazine(): base()
        {
            Number = string.Empty;
            Title = string.Empty;
            DatePublished = DateTime.MinValue;
            Publisher = string.Empty;
            ReleaseNumber = 0;
        }

        public Magazine(string number, string title, DateTime datePublished, string publisher, int releaseNumber): base(number, title, datePublished)
        {
            Publisher = publisher;
            ReleaseNumber = releaseNumber;
        }

        public bool Equals(Magazine? other)
        {
            return base.Equals(other) && Publisher == other.Publisher && ReleaseNumber == other.ReleaseNumber;
        }
    }
}