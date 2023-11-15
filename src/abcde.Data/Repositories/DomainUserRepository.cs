using abcde.Data.Interfaces;
using abcde.Data.Repositories.Base;
using abcde.Model.Base;
using abcde.Model;

namespace abcde.Data.Repositories
{
    public class DomainUserRepository: GenericAsyncRepository<DomainUser, BaseSummary, BaseFilter>, IDomainUserRepository
    {
        public DomainUserRepository(DataContext context) : base(context)
        {            
        }
    }
}
