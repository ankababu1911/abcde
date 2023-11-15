using abcde.Model.Base;
using System.Linq.Expressions;

namespace abcde.Data.Interfaces.Base
{
    public interface IGenericAsyncRepository<TEntity, TSummary, in TFilter> where TEntity : BaseEntity
    {
        Task<int> GetCountAsync();

        Task<TEntity> GetFirstAsync();

        Task<TEntity> GetLastAsync();

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TSummary>> GetAllSummaryAsync();

        Task<TEntity> GetAsync(Guid entityId);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TSummary> UpdateSummaryAsync(TSummary entity);

        Task<IEnumerable<TEntity>> GetFilteredAsync(TFilter filter);

        Task<IEnumerable<TSummary>> GetFilteredSummaryAsync(TFilter filter);

        Task<TSummary> GetSummaryAsync(Guid id);

        IEnumerable<TEntity> GetAll();

        Task DeleteAsync(Guid id, string lastModifiedBy);

        Task UpdateAudit(TEntity updatedEntity);

        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);

        void UpdateDatabase();
    }
}