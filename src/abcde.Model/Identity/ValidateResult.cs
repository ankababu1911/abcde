namespace abcde.Model.Identity
{
    public class ValidateResult
    {
        public bool Successful { get; set; }
        public string UserId { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Token { get; set; }
    }
}
