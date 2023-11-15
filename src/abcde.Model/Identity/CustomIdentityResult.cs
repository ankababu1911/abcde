namespace abcde.Model.Identity
{
    public class CustomIdentityResult
    {
        public bool Succeeded { get; set; }
        public ICollection<IdentityError> Errors { get; set; } = new List<IdentityError>();
    }
}
