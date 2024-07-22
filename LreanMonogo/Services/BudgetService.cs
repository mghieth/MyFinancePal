using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface IBudgetService
    {
        public Task<List<Budget>> GetAllAsync(string userId);

        public Task<Budget?> GetAsync(string id);
        public Task CreateBudget(Budget newBudget);

        public Task UpdateBudget(string id, Budget updateBudget);

        public Task ViewBudget();
    }

    public class BudgetService:IBudgetService
    {
        private readonly IMongoCollection<Budget> _budgetCollection;

        public BudgetService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _budgetCollection = mongoDatabase.GetCollection<Budget>(
                bookStoreDatabaseSettings.Value.BudgetCollectionName);
        }

        public async Task<List<Budget>> GetAllAsync(string userId)
        {
            var budgets = await _budgetCollection.Find(x => x.UserId == userId).ToListAsync();
            return budgets;
        }

        public async Task<Budget?> GetAsync(string id) => await _budgetCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateBudget(Budget newBudget)
        {
            await _budgetCollection.InsertOneAsync(newBudget);
        }

        public async Task UpdateBudget(string id,Budget updateBudget)
        {
            await _budgetCollection.ReplaceOneAsync(x => x.Id == id, updateBudget);
        }

        public Task ViewBudget()
        {
            throw new NotImplementedException();
        }
    }
}
