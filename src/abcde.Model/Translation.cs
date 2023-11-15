using abcde.Model.Base;

namespace abcde.Model
{
    public class Translation :BaseEntity
    {
        public string Key { get; set; }
        public string LanguageCode { get; set; }
        public string Value { get; set; }
    }
}
