using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyFinancePal.Models
{
    public class Investment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? InvestmentType { get; set; }

        public double AmountInvested { get; set; }

        public double CurrentValue { get; set; }

        public DateTime DateOfInvestment { get; set; }
    }
}
