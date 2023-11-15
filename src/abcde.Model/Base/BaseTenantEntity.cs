namespace abcde.Model.Base
{
    public class BaseTenantEntity : BaseEntity
    {
        /// <summary>
        /// Tenant level identifier
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Location, office or branch level
        /// </summary>
        public string SubTenantId { get; set; }

        /// <summary>
        /// Department level
        /// </summary>
        public string DepartmentId { get; set; }
    }
}
