using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentCabinetLibrary
{
    [Serializable]
    [JsonDerivedType(typeof(Document), typeDiscriminator: "Document")]
    [JsonDerivedType(typeof(Book), typeDiscriminator: "Book")]
    [JsonDerivedType(typeof(LocalizedBook), typeDiscriminator: "Localized book")]
    [JsonDerivedType(typeof(Patient), typeDiscriminator: "Patient")]
    [JsonDerivedType(typeof(Magazine), typeDiscriminator: "Magazine")]
    public class Document: IEquatable<Document>
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public DateTime DatePublished { get; set; }

        public Document()
        {
            Number = string.Empty;
            Title = string.Empty;
            DatePublished = DateTime.MinValue;
        }

        public Document(string number, string title, DateTime datePublished)
        {
            Number = number;
            Title = title;
            DatePublished = datePublished;
        }

        public bool Equals(Document? other) 
        {
            if (other == null) return false;

            return Number == other.Number && Title == other.Title && DatePublished.Equals(other.DatePublished);
        }

        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}