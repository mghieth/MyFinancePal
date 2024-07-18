namespace MyFinancePal.Models
{
    public class Budget
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime Month { get; set; }

        public double TotalBudget { get; set; }

        public Dictionary<string,Double> AllocatedAmounts { get; set; }
    }
}
