using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MyFinancePal.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("CourseName")]
        [JsonPropertyName("CourseName")]
        public string CourseName { get; set; } = null!;
    }
}
