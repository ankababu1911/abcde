using abcde.Data.Interfaces.Base;
using abcde.Model.Base;
using abcde.Model;
using abcde.Model.Filters;

namespace abcde.Data.Interfaces
{
    public interface IUserRepository : IGenericTenantAsyncRepository<User, BaseTenantSummary, UserFilter>
    {
        Task<bool> CheckUserExistsByEmail(string email);
    }
}
