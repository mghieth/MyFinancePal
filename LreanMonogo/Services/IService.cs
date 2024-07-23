using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface IService<T>
    {
        public Task<List<T>> GetAllAsync(string userId);

        public Task<T?> GetAsync(string id);
        public Task Create(T newT);

        public Task Update(string id, T updateT);

        public Task Remove(string id);

        public Task View();
    }
}
