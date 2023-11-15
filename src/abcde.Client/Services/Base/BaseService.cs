using System;
using System.Globalization;
using abcde.Client.Helpers;
using abcde.Model.Base;
using abcde.Model.Dtos;

namespace abcde.Client.Services.Base
{
    public abstract class BaseService<TEntity, TSummary, TFilter> : BaseClient
        where TEntity : BaseEntity, new()
        where TSummary : new()
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

        public BaseService(HttpClient httpClient) : base(httpClient)
        { }

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
        public virtual async Task<TEntity> GetInstanceAsync(Guid id)
        {
            return id == Guid.Empty ?
                new TEntity() { IsActive = true, Created = DateTime.Now, Datestamp = DateTime.Now } :
                await GetAsync(id);
        }

        public virtual async Task<TEntity> GetAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceGet);

            return await GetAsync<TEntity>(url);
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
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

        public virtual async Task<TSummary> GetSummaryAsync(int id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceSummary, id);

            return await GetAsync<TSummary>(url);
        }

        public virtual async Task<TSummary> GetSummaryByStringAsync(string id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", BaseResource, resourceByStringSummary, id);

            return await GetAsync<TSummary>(url);
        }

        public async Task<TSummary> GetSummaryAsync()
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceGetSummary);

            return await GetAsync<TSummary>(url);
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            return entity.Id.Equals(Guid.Empty) ? await AddAsync(entity) : await UpdateAsync(entity);
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

        public async Task DeleteAsync(Guid id)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, id);

            await DeleteAsync(url);
        }

        public async Task<IEnumerable<TEntity>> InsertAll(IEnumerable<TEntity> items)
        {
            var url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", BaseResource, resourceAddRange);

            return await PostEnumerableAsync(url, items);
        }
    }
}