using LearnMongo.Models;

namespace LearnMongo.Resource
{
    public class StudentResource
    {
        public string StudentName { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public IList<Course> Courses { get; set; }
        public Book Book { get; set; }
    }
}
