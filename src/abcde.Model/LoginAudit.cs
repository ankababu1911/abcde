using abcde.Model.Base;

namespace abcde.Model
{
    public class LoginAudit : BaseTenantEntity
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
