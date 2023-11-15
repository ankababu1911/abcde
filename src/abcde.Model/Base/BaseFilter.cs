namespace abcde.Model.Base
{
    public class BaseFilter
    {
        public Guid Id { get; set; }
        public bool? IsActive { get; set; }
        public string TenantId { get; set; }
    }
}
