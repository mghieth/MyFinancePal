using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;
using System.Transactions;

namespace MyFinancePal.Services
{

    public interface ICategoryService : IService<Category>
    {

    }
    public class CategoryService :ICategoryService
    {
        private readonly IMongoCollection<Category> _categoriesCollection;

        public CategoryService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _categoriesCollection = mongoDatabase.GetCollection<Category>(
                bookStoreDatabaseSettings.Value.CategoriesCollectionName);
        }

        public async Task Create(Category newT)
        {
            await _categoriesCollection.InsertOneAsync(newT);
        }

        public async Task<List<Category>> GetAllAsync(string userId)
        {
            var defaultCategories = await _categoriesCollection.Find(x => string.IsNullOrEmpty(x.UserId) ).ToListAsync();

            var spicificUser = await _categoriesCollection.Find(x => x.UserId == userId).ToListAsync();
            if (spicificUser.Any())
            {
                defaultCategories.AddRange(spicificUser);

            }
            return defaultCategories;

        }

        public async Task<Category?> GetAsync(string id) => await _categoriesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Remove(string id)
        {
            await _categoriesCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task Update(string id, Category updateT)
        {
            await _categoriesCollection.ReplaceOneAsync(x => x.Id == id, updateT);

        }

        public Task View()
        {
            throw new NotImplementedException();
        }
    }
}
