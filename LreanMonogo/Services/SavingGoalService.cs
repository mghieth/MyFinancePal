using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface ISavingGoalService:IService<SavingGoal>
    {
        public Task TrackGoal();

    }
    public class SavingGoalService : ISavingGoalService
    {
        private readonly IMongoCollection<SavingGoal> _savingGoalCollection;

        public SavingGoalService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _savingGoalCollection = mongoDatabase.GetCollection<SavingGoal>(
                bookStoreDatabaseSettings.Value.SavingGoalCollectionName);
        }
        public async Task<List<SavingGoal>> GetAllAsync(string userId)
        {
            var savingGoals = await _savingGoalCollection.Find(x => x.UserId == userId).ToListAsync();

            return savingGoals;
        }

        public async Task<SavingGoal?> GetAsync(string id) => await _savingGoalCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        

        public async Task Create(SavingGoal newGoal)
        {
            await _savingGoalCollection.InsertOneAsync(newGoal);

        }

        public async Task Update(string id, SavingGoal updateGoal)
        {
            await _savingGoalCollection.ReplaceOneAsync(x => x.Id == id, updateGoal);
        }

        public async Task Remove(string id)
        {
            await _savingGoalCollection.DeleteOneAsync(x => x.Id == id);
        }

        public Task View()
        {
            throw new NotImplementedException();
        }

        public Task TrackGoal()
        {
            throw new NotImplementedException();
        }
    }
}
