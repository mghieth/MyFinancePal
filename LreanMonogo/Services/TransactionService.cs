using MyFinancePal.Resource;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;
using MongoDB.Driver.Linq;

namespace MyFinancePal.Services
{
    public interface ITransactionService : IService<Transactions>
    {
        public Task<List<Transactions>> GetAllAsync(string userId, TransactionFilter filter);

    }

    public class TransactionService : ITransactionService
    {
        private readonly IMongoCollection<Transactions> _transactionsCollection;

        private readonly IBudgetService _budgetService;


        public TransactionService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings, IBudgetService budgetService)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _transactionsCollection = mongoDatabase.GetCollection<Transactions>(
                bookStoreDatabaseSettings.Value.TransactionsCollectionName);

            _budgetService = budgetService;
        }

        public async Task<List<Transactions>> GetAllAsync(string userId)
        {
            var transactions = await _transactionsCollection.Find(x => x.UserId == userId).ToListAsync();

            return transactions;
        }

        public async Task<List<Transactions>> GetAllAsync(string userId, TransactionFilter filter)
        {

            var transactions = await _transactionsCollection.Find(x => x.UserId == userId).ToListAsync();

            if(filter is not null)
            {
                transactions= transactions.Where(x=>x.Date >= filter.FromDate.AddDays(-1) && x.Date <= filter.ToDate).ToList();

                if(filter.Category is not null)
                {
                    transactions= transactions.Where(x=> (x.Type == "Expense" && x.Category == filter.Category) || x.Type == "Income").ToList();
                }
            }

            return transactions;
        }


        public async Task<Transactions?> GetAsync(string id) => await _transactionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
      

        public async Task Create(Transactions newTran)
        {
            if(newTran.Type == "Expense") 
                TrackingBudget(newTran);         
            
            await _transactionsCollection.InsertOneAsync(newTran);
        }

        public async Task Update(string id, Transactions updateTran)
        {          
            await _transactionsCollection.ReplaceOneAsync(x => x.Id == id, updateTran);

            if (updateTran.Type == "Expense")
                TrackingBudget(updateTran);
        }

        public async Task Remove(string id)
        {
            var transaction = await GetAsync(id);
            
            await _transactionsCollection.DeleteOneAsync(x => x.Id == id);

            if (transaction?.Type == "Expense")
                TrackingBudget(transaction);
        }

        public Task View()
        {
            throw new NotImplementedException();
        }


        private async void TrackingBudget(Transactions transaction)
        {
            var budget = (await _budgetService.GetAllAsync(transaction.UserId)).FirstOrDefault( x =>
                x.Category == transaction.Category &&
                x.Month.Year == transaction.Date.Year && 
                x.Month.Month == transaction.Date.AddHours(4).Month);

            if (budget != null)
            {
                budget.RemainingAmount = _budgetService.GetRemainingAmount(budget);
                await _budgetService.Update(budget.Id, budget);
            }
        }                  
    }
}
