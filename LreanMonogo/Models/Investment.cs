namespace MyFinancePal.Models
{
    public class Investment
    {
        public string Id { get; set; }

        public string UserID { get; set; }

        public string InvestmentType { get; set; }

        public double AmountInvested { get; set; }

        public double CurrentValue { get; set; }

        public DateTime DateOfInvestment { get; set; }
    }
}
