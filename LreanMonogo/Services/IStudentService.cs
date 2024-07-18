using LearnMongo.Models;
using LearnMongo.Resource;

namespace LearnMongo.Services
{
    public interface IStudentService
    {
        public  Task<List<StudentResource>> GetAsync();

        public  Task<StudentResource?> GetAsync(string id);

        public Task CreateAsync(Student newStudent);

        public  Task UpdateAsync(string id, Student updatedStudent);

        public  Task RemoveAsync(string id);

    }
}
