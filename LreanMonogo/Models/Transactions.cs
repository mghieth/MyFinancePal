﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyFinancePal.Models
{
    public class Transactions
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? Type { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? PaymentMethod { get; set; }

        public string? Source { get; set; }

    }

    public class TransactionFilter
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string? Category { get; set; }

    }

    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? Name { get; set; }
    }
}
