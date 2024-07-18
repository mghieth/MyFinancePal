namespace MyFinancePal.Models
{
    public class Report
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ReportType { get; set; }

        public DateTime GeneratedDate { get; set; }

        public string ReportData { get; set; }
    }
}
