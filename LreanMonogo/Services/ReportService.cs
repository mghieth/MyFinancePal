namespace MyFinancePal.Services
{
    public interface IReportService
    {
        public void GenerateReport();

        public void ViewReport();
    }
    public class ReportService : IReportService
    {
        public void GenerateReport()
        {
            throw new NotImplementedException();
        }

        public void ViewReport()
        {
            throw new NotImplementedException();
        }
    }
}
