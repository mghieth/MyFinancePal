namespace MyFinancePal.Models
{
    public class FinancialEducationContent
    {
        public string Id { get; set; }

        public string AdminId { get; set; }

        public string Title { get; set; }

        //e.g., article, video
        public string ContentType { get; set; }

        public string Content { get; set; }

        public DateTime PublicationDate { get; set; }

    }
}
