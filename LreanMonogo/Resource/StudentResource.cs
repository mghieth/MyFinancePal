using LearnMongo.Models;

namespace LearnMongo.Resource
{
    public class StudentResource
    {
        public string? Id { get; set; }

        public string StudentName { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public IList<string>? Courses { get; set; }
        public string? BookName { get; set; }
    }
}
