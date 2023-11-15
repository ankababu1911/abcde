namespace abcde.Model.Base
{
    public class BaseTenantFilter
    {
        public Guid Id { get; set; }
        public bool? IsActive { get; set; }
        public string TenantId { get; set; }
        public string SubTenantId { get; set; }
        public string DepartmentId { get; set; }

    }
}
