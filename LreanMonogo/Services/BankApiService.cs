namespace MyFinancePal.Services
{

    public interface IBankApiService
    {
        public void ConnectToBank();

        public void FetchTransaction();

        public void SyncData();
    }
    public class BankApiService
    {
    }
}
