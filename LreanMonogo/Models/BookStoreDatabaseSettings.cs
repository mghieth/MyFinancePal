namespace MyFinancePal.Models
{
    public class BookStoreDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksCollectionName { get; set; } = null!;

        public string StudentsCollectionName { get; set; } = null!;

        public string CoursesCollectionName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string TransactionsCollectionName { get; set; } = null!;

        public string BudgetCollectionName { get; set; } = null!;

        public string SavingGoalCollectionName { get; set; } = null!;
        public string InvestmentCollectionName { get; set; } = null!;

        public string DebtCollectionName { get; set; } = null!;

    }
}
