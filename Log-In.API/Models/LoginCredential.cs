using MongoDB.Bson.Serialization.Attributes;

namespace Log_In.API.Models
{
    public class LoginCredential
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Salt { get; set; }
    }
}
