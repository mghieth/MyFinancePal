namespace LearnMongo.Models
{
    public class BookStoreDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksCollectionName { get; set; } = null!;
        public string StudentsCollectionName { get; set; } = null!;
        public string CoursesCollectionName { get; set; } = null!;

    }
}
