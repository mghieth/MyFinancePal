﻿using LearnMongo.Models;

namespace LearnMongo.Services
{
    public class IncomeService: ITransactionService
    {
        public Task<List<Transactions>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Transactions?> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task AddTransaction(Transactions newTran)
        {
            throw new NotImplementedException();
        }

        public Task EditTransaction(string id, Transactions updateTran)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTransaction(string id)
        {
            throw new NotImplementedException();
        }
    }
}
