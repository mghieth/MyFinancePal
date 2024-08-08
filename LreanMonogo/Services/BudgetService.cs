using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface IBudgetService:IService<Budget>
    {
        public double GetRemainingAmount(Budget budget);

    }

    public class BudgetService:IBudgetService
    {
        private readonly IMongoCollection<Budget> _budgetCollection;
        private readonly IMongoCollection<Transactions> _transactionsCollection;



        public BudgetService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _budgetCollection = mongoDatabase.GetCollection<Budget>(
                bookStoreDatabaseSettings.Value.BudgetCollectionName);

            _transactionsCollection = mongoDatabase.GetCollection<Transactions>(
                bookStoreDatabaseSettings.Value.TransactionsCollectionName);
        }

        public async Task<List<Budget>> GetAllAsync(string userId)
        {
            var budgets = await _budgetCollection.Find(x => x.UserId == userId).ToListAsync();

            return budgets;
        }

        public async Task<Budget?> GetAsync(string id) => await _budgetCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Create(Budget newBudget)
        {
            newBudget.RemainingAmount = GetRemainingAmount(newBudget);
            await _budgetCollection.InsertOneAsync(newBudget);
        }

        public async Task Update(string id,Budget updateBudget)
        {
            updateBudget.RemainingAmount = GetRemainingAmount(updateBudget);
            await _budgetCollection.ReplaceOneAsync(x => x.Id == id, updateBudget);
        }

        public async Task Remove(string id)
        {
            await _budgetCollection.DeleteOneAsync(x => x.Id == id);
        }

        public Task View()
        {
            throw new NotImplementedException();
        }

        public double GetRemainingAmount(Budget budget)
        {
            var transactionsAmount = 0.00;

            var transactions = _transactionsCollection.Find(x => x.UserId == budget.UserId && x.Type == "Expense" && x.Category == budget.Category ).ToList();
           
             foreach (var transaction in transactions)
             {
                    if (budget.Month.Year == transaction.Date.Year && budget.Month.Month  == transaction.Date.AddHours(4).Month)
                    {
                    transactionsAmount += transaction.Amount;
                    }
             }
             return transactionsAmount == 0.00 ? budget.TotalBudget : budget.TotalBudget - transactionsAmount;
        }
        
    }
}
