using System.Globalization;
using Microsoft.Extensions.Caching.Memory;
using abcde.Client.Helpers;
using abcde.Model.Base;

namespace abcde.Client.Services.Base
{
    public abstract class BaseTenantService<TEntity, TSummary, TFilter> : BaseClient
        where TEntity : BaseTenantEntity, new()
        where TSummary : new()
        where TFilter : BaseTenantFilter, new()
    {
        protected string BaseResource;

        private const string resourceFirst = "first";
        private const string resourceLast = "last";
        private const string resourceAll = "all";
        private const string resourceCount = "count";
        private const string resourceSummaryFiltered = "summary/filtered";
        private const string resourceFiltered = "filtered";
        private const string resourceSummaryAll = "summary/all";
        private const string resourceSummary = "summary";
        private const string resourceByStringSummary = "summaryByString";
        private const string resourceGet = "getAsync";
        private const string resourceByString = "byString";
        private const string resourceBySystemId = "bySystemId";
        private const string resourceGetSummary = "getSummary";
        private const string resourceElasticSearch = "ES";
        private const string resourceAddRange = "AddRange";
        private const string resourceUpdateRange = "UpdateRange";


        protected readonly IMemoryCache memoryCache;

        protected const string MemCacheGet = "Get";
        protected const string MemCacheGetAll = "GetAll";

        public BaseTenantService(HttpClient httpClient, IMemoryCache memoryCache) : base(httpClient)
        {
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Used in testing
        /// </summary>
        /// <param name="httpClient"></param>
        public void SetHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Get instance regardless of new or existing TEntity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetInstanceAsync(int id)
        {
            return id == 0 ?
                new TEntity() { IsActive = true, Created = DateTime.Now, Datestamp = DateTime.Now } :
                await GetAsync(id);
        }

        public virtual async Task<TEntity> GetAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceGet);

            return await GetAsync<TEntity>(url);
        }

        public virtual async Task<TEntity> GetMemCacheAsync(string tenant)
        {
            var key = $"{BaseResource}{tenant}{MemCacheGet}";

            var encodedCache = memoryCache.Get(key);

            if (encodedCache == null)
            {
                var result = await GetAsync();

                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));

                memoryCache.Set(key, result, options);

                return result;
            }
            else
            {
                return (TEntity)memoryCache.Get(key);
            }
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, id);

            return await GetAsync<TEntity>(url);
        }

        public virtual async Task<TEntity> GetAsyncString(string id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceByString, id);

            return await GetAsync<TEntity>(url);
        }

        public virtual async Task<TEntity> GetBySystemIdAsync(string systemId)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceBySystemId, systemId);

            return await GetAsync<TEntity>(url);
        }

        public async Task<int> GetCountAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceCount);

            return await GetAsync<int>(url);
        }

        public async Task<TEntity> GetFirstAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceFirst);

            return await GetAsync<TEntity>(url);
        }

        public async Task<TEntity> GetLastAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceLast);

            return await GetAsync<TEntity>(url);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceAll);

            return await GetIEnumerableAsync<TEntity>(url);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllMemCacheAsync(string tenant)
        {
            var key = $"{BaseResource}{tenant}{MemCacheGetAll}";

            var encodedCache = memoryCache.Get(key);

            if (encodedCache == null)
            {
                var result = await GetAllAsync();

                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));

                memoryCache.Set(key, result, options);

                return result;
            }
            else
            {
                return (IEnumerable<TEntity>)memoryCache.Get(key);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllActiveMemCacheAsync(string tenant)
        {
            var key = $"{BaseResource}{tenant}{MemCacheGetAll}";

            var encodedCache = memoryCache.Get(key);

            if (encodedCache == null)
            {
                var result = await GetFilteredAsync(new TFilter() { IsActive = true });

                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(1));

                memoryCache.Set(key, result, options);

                return result;
            }
            else
            {
                return (IEnumerable<TEntity>)memoryCache.Get(key);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllByStringAsync(string resource)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resource);

            return await GetIEnumerableAsync<TEntity>(url);
        }

        public virtual async Task<IEnumerable<TSummary>> GetAllSummaryAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceSummaryAll);

            return await GetIEnumerableAsync<TSummary>(url);
        }

        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync(TFilter filter)
        {
            var queryString = filter.ToQueryString();

            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}?{2}", BaseResource, resourceFiltered, queryString);

            return await GetIEnumerableAsync<TEntity>(url);
        }

        public async Task<IEnumerable<TSummary>> GetFilteredSummaryAsync(TFilter filter)
        {
            var queryString = filter.ToQueryString();

            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}?{2}", BaseResource, resourceSummaryFiltered, queryString);

            return await GetIEnumerableAsync<TSummary>(url);
        }

        public async Task<IEnumerable<TSummary>> GetSearchSummaryAsync(string search)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceElasticSearch, search);

            return await GetIEnumerableAsync<TSummary>(url);
        }

        public async virtual Task<TSummary> GetSummaryAsync(int id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceSummary, id);

            return await GetAsync<TSummary>(url);
        }

        public async virtual Task<TSummary> GetSummaryByStringAsync(string id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceByStringSummary, id);

            return await GetAsync<TSummary>(url);
        }

        public async Task<TSummary> GetSummaryAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceGetSummary);

            return await GetAsync<TSummary>(url);
        }

        /// <summary>
        /// Try clearing mem cache for Get and GetAll
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            ClearMemCache($"{BaseResource}{entity.TenantId}{MemCacheGet}");
            ClearMemCache($"{BaseResource}{entity.TenantId}{MemCacheGetAll}");

            return entity.Id == Guid.Empty ? await UpdateAsync(entity) : await AddAsync(entity);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}", BaseResource);

            return await PostAsync(url, entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}", BaseResource);

            return await PutAsync(url, entity);
        }

        public async Task<TSummary> UpdateSummaryAsync(TSummary entity)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceSummary);

            return await PutAsync(url, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, id);

            await DeleteAsync(url);
        }

        public async Task<IEnumerable<TEntity>> InsertAll(IEnumerable<TEntity> items)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceAddRange);

            return await PostEnumerableAsync(url, items);
        }

        public async Task<IEnumerable<TEntity>> SaveAllAsync(IEnumerable<TEntity> items)
        {
            ClearMemCache($"{BaseResource}{items.First().TenantId}{MemCacheGetAll}");

            var newItems = items.Where(x => x.Id == Guid.Empty).ToList();
            var existingItems = items.Where(x => x.Id != Guid.Empty).ToList();

            if (newItems.Count() > 0)
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceAddRange);

                await PostEnumerableAsync(url, newItems);
            }

            if (existingItems.Count() > 0)
            {
                var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceUpdateRange);

                await PutEnumerableAsync(url, existingItems);
            }
            return items;
        }

        /// <summary>
        /// Try and clear memcache if exists
        /// </summary>
        /// <param name="key"></param>
        protected void ClearMemCache(string key)
        {
            var encodedCache = memoryCache.Get(key);

            if (encodedCache != null)
            {
                memoryCache.Remove(key);
            }
        }
    }
}
