namespace abcde.Model.Identity
{
    public class IdentityResult
    {
        public string Id { get; set; }
        public bool Succeeded { get; set; }
        public ICollection<IdentityError> Errors { get; set; } = new List<IdentityError>();
    }
}
