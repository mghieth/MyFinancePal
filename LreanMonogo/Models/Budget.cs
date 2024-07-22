using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyFinancePal.Models
{
    public class Budget
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? UserId { get; set; }
            
        public DateTime Month { get; set; }

        public double TotalBudget { get; set; }

        public Dictionary<string,Double> AllocatedAmounts { get; set; }
    }
}
