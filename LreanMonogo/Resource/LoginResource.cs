namespace MyFinancePal.Resource
{
    public class LoginResource
    {
        public string? UserId { get; set; }

        public bool Result { get; set; } = false;

        public string Token { get; set; } = "";

        public bool EmailAlreadyExist { get; set; } = false;

        public string? Error { get; set; }
    }
}
