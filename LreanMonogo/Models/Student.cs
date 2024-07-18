using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace LearnMongo.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("StudentName")]
        [JsonPropertyName("StudentName")]
        public string StudentName { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public IList<Course>? Courses { get; set; }

        public string? BookId { get; set; }

        public Book? Book { get; set; }
    }
}
