namespace abcde.Model.Validation
{
    public class ValidationErrors
    {
        public string Message { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }
    }
}
