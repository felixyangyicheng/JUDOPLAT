using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace JUDOPLAT.API_JUDOPLAT.Data
{
    public class PdfModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FileName { get; set; }
        public string Category { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfUpdate { get; set; }
        public byte[] Content { get; set; }
    }
}
