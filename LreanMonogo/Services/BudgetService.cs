using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface IBudgetService:IService<Budget>
    {
       
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

        public async Task Create(Budget newBudget)
        {
            await _budgetCollection.InsertOneAsync(newBudget);
        }

        public async Task Update(string id,Budget updateBudget)
        {
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
    }
}
