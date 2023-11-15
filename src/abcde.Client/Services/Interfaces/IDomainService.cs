using abcde.Model;
using abcde.Model.Base;
using abcde.Model.Identity;

namespace abcde.Client.Services.Interfaces
{
    public interface IDomainService : IGenericService<Domain, BaseSummary, BaseFilter>
    {
    }
}