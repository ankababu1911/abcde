using Microsoft.EntityFrameworkCore;
using abcde.Data.Interfaces;
using abcde.Data.Repositories.Base;
using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Filters;

namespace abcde.Data.Repositories
{
    public class UserRepository : GenericTenantAsyncRepository<User, BaseTenantSummary, UserFilter>, IUserRepository
    {
        private readonly DataContext _context;

        #region ctor
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// Check if Email exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserExistsByEmail(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

    }
}
