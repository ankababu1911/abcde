namespace abcde.Model.Identity
{
    public class RegisterResult
    {
        public bool Successful { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public Guid TenantId { get; set; }

        public string Token { get; set; }

        public string ConnectionStringCode { get; set; }
    }
}