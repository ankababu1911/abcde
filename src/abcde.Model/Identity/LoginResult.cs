namespace abcde.Model.Identity
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public bool HasChangedPassword { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public string TenantId { get; set; }
        public string TenantConnCode { get; set; }
    }
}