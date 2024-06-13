using LearnMongo.Models;

namespace LearnMongo.Services
{
    public interface IStudentService
    {
        public  Task<List<Student>> GetAsync();

        public  Task<Student?> GetAsync(string id);

        public  Task CreateAsync(Student newStudent);

        public  Task UpdateAsync(string id, Student updatedStudent);

        public  Task RemoveAsync(string id);

    }
}
