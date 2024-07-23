using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyFinancePal.Models
{
    public class SavingGoal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string GoalName { get; set; }

        public double TargetAmount { get; set; }

        public double CurrentAmount { get; set; }

        public DateTime Deadline { get; set; }
    }
}
