using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyFinancePal.Models
{
    public class Debt
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string DebtType { get; set; }

        public double PrincipalAmount { get; set; }

        public double InterestRate { get; set; }

        public double RemainingAmount { get; set; }

        public DateTime DueDate { get; set; }

    }

}
