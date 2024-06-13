using LearnMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LearnMongo.Services
{
    public interface ICoursesService
    {
        public Task<List<Course>> GetAsync();

        public Task<Course?> GetAsync(string id);

        public Task<List<Student>> GetStudentsAsync(string id);

        //public Task CreateAsync(Course newCourse);

        //public Task UpdateAsync(string id, Student updatedCourse);

        //public Task RemoveAsync(string id);
    }


    public class CoursesService : ICoursesService
    {
        private readonly IMongoCollection<Student> _studentsCollection;
        private readonly IMongoCollection<Course> _CoursesCollection;

        public CoursesService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _studentsCollection = mongoDatabase.GetCollection<Student>(
                bookStoreDatabaseSettings.Value.StudentsCollectionName);

            _CoursesCollection =
                mongoDatabase.GetCollection<Course>(bookStoreDatabaseSettings.Value.CoursesCollectionName);
        }

        public async Task<List<Course>> GetAsync() => 
            await _CoursesCollection.Find(_ => true).ToListAsync();

        public async Task<Course?> GetAsync(string id) =>
            await _CoursesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
       

        public async Task<List<Student>> GetStudentsAsync(string courseName)
        {
            var students = await _studentsCollection.Find(_ => true).ToListAsync();
            var lis = new List<Student>();
            foreach (var student in students)
            {
                if(student.Courses != null)
                    foreach (var course in student.Courses)
                    {
                        if (course.CourseName == courseName)
                        {
                            lis.Add(student);
                        }
                    }
            }

            return lis;
        }
    }
}
