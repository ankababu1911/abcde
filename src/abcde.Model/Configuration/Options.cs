namespace abcde.Model.Configuration
{
    public class Options
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
        public ApplicationSettings ApplicationSettings { get; set; }
    }

    public class ApplicationSettings
    {
        public JWTSettings JWTSettings { get; set; }
    }

    public class JWTSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        /// <summary>
        /// In hours
        /// </summary>
        public int Expiry { get; set; }
    }
}