namespace abcde.Model.Validation
{
    public class Error
    {
        public string field { get; set; }
        public string message { get; set; }
    }

    public class ValidationError
    {
        public string message { get; set; }
        public List<Error> errors { get; set; }
    }
}
