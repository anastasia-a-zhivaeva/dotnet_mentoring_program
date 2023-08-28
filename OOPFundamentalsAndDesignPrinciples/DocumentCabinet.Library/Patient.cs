namespace DocumentCabinetLibrary
{
    [Serializable]
    public class Patient : Document, IEquatable<Patient>
    {
        public List<string> Authors { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ID { get; set; }

        public Patient(): base()
        {
            Authors = new List<string>();
            ExpirationDate = DateTime.MinValue;
            ID = string.Empty;
        }

        public Patient(string number, string title, DateTime datePublished, List<string> authors, DateTime expirationDate, string id): base(number, title, datePublished)
        {
            Authors = authors;
            ExpirationDate = expirationDate;
            ID = id;
        }

        public bool Equals(Patient? other)
        {
            return base.Equals(other) && Authors.SequenceEqual(Authors) && ExpirationDate.Equals(other.ExpirationDate) && ID == other.ID;
        }
    }
}