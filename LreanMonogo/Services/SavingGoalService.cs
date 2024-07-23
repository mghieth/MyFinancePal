using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyFinancePal.Models;

namespace MyFinancePal.Services
{
    public interface ISavingGoalService
    {
        public Task<List<SavingGoal>> GetAllAsync(string userId);

        public Task<SavingGoal?> GetAsync(string id);

        public Task CreateGoal( SavingGoal newGoal);

        public Task UpdateGoal(string id,SavingGoal updateGoal);

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
        

        public async Task CreateGoal(SavingGoal newGoal)
        {
            await _savingGoalCollection.InsertOneAsync(newGoal);

        }

        public async Task UpdateGoal(string id, SavingGoal updateGoal)
        {
            await _savingGoalCollection.ReplaceOneAsync(x => x.Id == id, updateGoal);
        }

        public Task TrackGoal()
        {
            throw new NotImplementedException();
        }
    }
}
