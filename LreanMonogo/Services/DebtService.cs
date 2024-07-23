using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface IDeptService: IService<Debt>
    {

    }

    public class DebtService : IDeptService
    {
        public Task<List<Debt>> GetAllAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Debt?> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Debt newT)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, Debt updateT)
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
    }
}
