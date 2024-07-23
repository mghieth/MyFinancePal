using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface IDebtService: IService<Debt>
    {

    }

    public class DebtService : IDebtService
    {
        private readonly IMongoCollection<Debt> _debtCollection;

        public DebtService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _debtCollection = mongoDatabase.GetCollection<Debt>(
                bookStoreDatabaseSettings.Value.DebtCollectionName);
        }

        public async Task<List<Debt>> GetAllAsync(string userId)
        {
            return await _debtCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Debt?> GetAsync(string id) => await _debtCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
       

        public async Task Create(Debt newDebt)
        {
            await _debtCollection.InsertOneAsync(newDebt);
        }

        public async Task Update(string id, Debt updateDebt)
        {
            await _debtCollection.ReplaceOneAsync(x => x.Id == id, updateDebt);
        }

        public async Task Remove(string id)
        {
            await _debtCollection.DeleteOneAsync(x => x.Id == id);
        }

        public Task View()
        {
            throw new NotImplementedException();
        }
    }
}
