using abcde.Model.Base;

namespace abcde.Model
{
    public class Audit : BaseTenantEntity
    {
        public Guid EntityId { get; set; }
        public string Entity { get; set; }
        public string FieldId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        //public string TenantId { get; set; }
    }
}
