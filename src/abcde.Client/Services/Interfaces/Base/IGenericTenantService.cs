namespace abcde.Client.Services.Interfaces.Base
{
    public interface IGenericTenantService<TEntity, TSummary, in TFilter> where TEntity : class
    {
        void SetHttpClient(HttpClient httpClient);

        /// <summary>
        /// Get First
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetFirstAsync();

        /// <summary>
        /// Get Last
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetLastAsync();

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllMemCacheAsync(string tenantId);

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllActiveMemCacheAsync(string tenantId);

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllByStringAsync(string resource);

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TSummary>> GetAllSummaryAsync();

        /// <summary>
        /// Get Filtered
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetFilteredAsync(TFilter filter);

        /// <summary>
        /// Get Filtered Summary
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TSummary>> GetFilteredSummaryAsync(TFilter filter);

        /// <summary>
        /// Get Entity instance using Id to either create new or get existing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetInstanceAsync(int id);

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetAsync();

        /// <summary>
        /// Get Entity using memcache
        /// </summary>
        /// <returns></returns>
        Task<TEntity> GetMemCacheAsync(string tenantId);

        /// <summary>
        /// Get by string Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsyncString(string id);

        /// <summary>
        /// Get by SystemId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetBySystemIdAsync(string systemId);

        /// <summary>
        /// Get Count
        /// </summary>
        /// <returns>Entity count</returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// Get Summary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TSummary> GetSummaryAsync(int id);

        /// <summary>
        /// Get Summary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TSummary> GetSummaryByStringAsync(string id);

        /// <summary>
        /// Get Summary
        /// </summary>
        /// <returns></returns>
        Task<TSummary> GetSummaryAsync();

        /// <summary>
        /// Generic method which determines whether to Add / Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> SaveAsync(TEntity entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TSummary> UpdateSummaryAsync(TSummary entity);

        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id"></param>
        Task DeleteAsync(int id);


        /// <summary>
        /// Insert All
        /// </summary>
        /// <param name="items"></param>
        Task<IEnumerable<TEntity>> InsertAll(IEnumerable<TEntity> items);


        /// <summary>
        /// Insert All
        /// </summary>
        /// <param name="items"></param>
        Task<IEnumerable<TEntity>> SaveAllAsync(IEnumerable<TEntity> items);

        ///// <summary>
        ///// Delete Range
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task DeleteRangeAsync(int[] id);
    }
}
