using abcde.Data.Interfaces.Base;
using abcde.Model;
using abcde.Model.Base;

namespace abcde.Data.Interfaces
{
    public  interface ILocalisationRepository:IGenericAsyncRepository<Translation,BaseSummary,BaseFilter>
    {
    }
}
