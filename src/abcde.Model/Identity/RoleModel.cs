namespace abcde.Model.Identity
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Datestamp { get; set; }
        public string TenantId { get; set; }

        public string SubTenantId { get; set; }
        public string DepartmentId { get; set; }
    }
}
