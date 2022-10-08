using MongoDB.Bson.Serialization.Attributes;

namespace Log_In.API.Models
{
    public class Session
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? id { get; set; }
        public string Email { get; set; }
        public string? Token { get; set; }
    }
}
