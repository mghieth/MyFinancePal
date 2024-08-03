using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public class IncomeService: ITransactionService
    {
        public Task<List<Transactions>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Transactions>> GetAllAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Transactions?> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Transactions newTran)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, Transactions updateTran)
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task View()
        {
            throw new NotImplementedException();
        }

        public Task<List<Transactions>> GetAllAsync(string userId, TransactionFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
