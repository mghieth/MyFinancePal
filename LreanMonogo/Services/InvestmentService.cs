using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface IInvestmentService: IService<Investment>
    {
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

        public async Task Create(Investment investment)
        {
            await _investmentCollection.InsertOneAsync(investment);
        }

        public async Task Update(string id, Investment updateInvestment)
        {
            await _investmentCollection.ReplaceOneAsync(x => x.Id == id, updateInvestment);
        }

        public async Task Remove(string id)
        {
            await _investmentCollection.DeleteOneAsync(x => x.Id == id);
        }

        public Task View()
        {
            throw new NotImplementedException();
        }

        public Task ViewInvestment()
        {
            throw new NotImplementedException();
        }
    }
}
