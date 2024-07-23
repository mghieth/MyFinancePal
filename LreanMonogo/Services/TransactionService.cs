using MyFinancePal.Resource;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface ITransactionService : IService<Transactions>
    {
       
    }

    public class TransactionService : ITransactionService
    {
        private readonly IMongoCollection<Transactions> _transactionsCollection;

        public TransactionService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _transactionsCollection = mongoDatabase.GetCollection<Transactions>(
                bookStoreDatabaseSettings.Value.TransactionsCollectionName);
        }

        public async Task<List<Transactions>> GetAllAsync(string userId)
        {
            var transactions = await _transactionsCollection.Find(x => x.UserId == userId).ToListAsync();

            return transactions;
        }

        public async Task<Transactions?> GetAsync(string id) => await _transactionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
      

        public async Task Create(Transactions newTran)
        {
            await _transactionsCollection.InsertOneAsync(newTran);
        }

        public async Task Update(string id, Transactions updateTran)
        {
             await _transactionsCollection.ReplaceOneAsync(x => x.Id == id, updateTran);
        }

        public async Task Remove(string id)
        {
            await _transactionsCollection.DeleteOneAsync(x => x.Id == id);
        }

        public Task View()
        {
            throw new NotImplementedException();
        }
    }
}
