using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface IInvestmentService
    {
        public Task<List<Investment>> GetAllAsync(string userId);

        public Task<Investment?> GetAsync(string id);

        public Task AddInvestment(Investment investment);

        public Task UpdateInvestment(string id, Investment updateInvestment);

        public Task ViewInvestment();
    }

    public class InvestmentService : IInvestmentService
    {
        private readonly IMongoCollection<Investment> _investmentCollection;

        public InvestmentService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _investmentCollection = mongoDatabase.GetCollection<Investment>(
                bookStoreDatabaseSettings.Value.InvestmentCollectionName);
        }

        public async Task<List<Investment>> GetAllAsync(string userId)
        {
            var investment = await _investmentCollection.Find(x => x.UserId == userId).ToListAsync();

            return investment;
        }

        public async Task<Investment?> GetAsync(string id) => await _investmentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task AddInvestment(Investment investment)
        {
            await _investmentCollection.InsertOneAsync(investment);
        }

        public async Task UpdateInvestment(string id, Investment updateInvestment)
        {
            await _investmentCollection.ReplaceOneAsync(x => x.Id == id, updateInvestment);
        }

        public Task ViewInvestment()
        {
            throw new NotImplementedException();
        }
    }
}
