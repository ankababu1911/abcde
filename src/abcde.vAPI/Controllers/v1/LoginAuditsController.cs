using abcde.Model;
using abcde.Data.Interfaces;
using abcde.Model.Summary;
using abcde.Model.Filters;
using abcde.vAPI.Controllers.Base;

namespace abcde.vAPI.Controllers.v1
{
    //[ServiceFilter(typeof(ValidateEntityExistsAttribute<BaseEntity>))]
    public class LoginAuditsController : GenericsController<LoginAudit, LoginAuditSummary, LoginAuditFilter>
    {
        public LoginAuditsController(ILoginAuditRepository repository) : base(repository)
        { }
    }
}
