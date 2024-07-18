namespace MyFinancePal.Models
{
    public class Debt
    {
        public string Id { get; set; }

        public string UserID { get; set; }

        public string DebtType { get; set; }

        public double PrincipalAmount { get; set; }

        public double InterestRate { get; set; }

        public double RemainingAmount { get; set; }

        public DateTime DueDate { get; set; }

    }

}
