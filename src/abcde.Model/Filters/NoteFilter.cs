using abcde.Model.Base;

namespace abcde.Model.Filters
{
    public class NoteFilter : BaseTenantFilter
    {
        public string DateString { get; set; }
        public Guid? UserId { get; set; }
    }
}