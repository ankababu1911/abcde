using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Microsoft.EntityFrameworkCore;
using abcde.Data.Interfaces.Base;
using abcde.Data.Predicates.Base;
using abcde.Model;
using abcde.Model.Base;
using Serilog;

namespace abcde.Data.Repositories.Base
{
    public class GenericTenantAsyncRepository<TEntity, TSummary, TFilter> : IGenericAsyncRepository<TEntity, TSummary, TFilter>
            where TEntity : BaseTenantEntity, new()
            where TSummary : BaseSummary, new()
            where TFilter : BaseTenantFilter, new()

    {
        protected IDbContextFactory<DataContext> DataContext;
        //protected DbContext Context;
        protected IDbContextFactory<DataContext> Context;
        internal DbSet<TEntity> DbSet;

        #region ctor

        protected GenericTenantAsyncRepository(IDbContextFactory<DataContext> context)
        {
            UpdateDatabase(context);
            DataContext = context;
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        protected GenericTenantAsyncRepository(IDbContextFactory<DataContext> context, IDbContextFactory<DataContext> contextFactory)
        {
            UpdateDatabase(context);
            DataContext = context;
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        #endregion ctor

        /// <summary>
        /// Get all Entities
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = (IQueryable<TEntity>)DbSet;

            return await Task.Run(() => query.ToList());
        }

        /// <summary>
        /// Get All Entities
        /// </summary>
        /// <returns></returns>
        public virtual Task<IEnumerable<TSummary>> GetAllSummaryAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await DbSet.FirstAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(string id)
        {
            return await DbSet.FirstAsync(x => x.TenantId == id);
        }

        /// <summary>
        /// Get Entity (by string)
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetStringAsync(string entityId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                entity.IsActive = true;
                entity.Datestamp = DateTime.Now;
                entity.Created = DateTime.Now;

                DbSet.Add(entity);
                await Context.SaveChangesAsync();
                await InsertAudit(new TEntity(), entity);

                return Context.Entry(entity).Entity;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                throw ex;
            }
        }

        /// <summary>
        /// Insert range
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Created = DateTime.Now;
                entity.Datestamp = DateTime.Now;
                entity.IsActive = true;
            }

            foreach (var entity in entities)
            {
                await InsertAudit(new TEntity(), entity);
            }

            DbSet.AddRange(entities);

            await Context.SaveChangesAsync();
            return entities;
        }

        /// <summary>
        /// Update range
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    entity.Datestamp = DateTime.Now;
                }

                foreach (var entity in entities)
                {
                    await UpdateAudit(entity);
                }

                DbSet.UpdateRange(entities);

                await Context.SaveChangesAsync();

                return entities;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                entity.Datestamp = DateTime.Now;

                DbSet.Update(entity);

                await UpdateAudit(entity);

                await Context.SaveChangesAsync();

                return Context.Entry(entity).Entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is TEntity)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            if (proposedValue != databaseValue)
                            {
                                proposedValues[property] = proposedValue;
                            }
                            else
                            {
                                proposedValues[property] = databaseValue;
                            }
                            //// TODO: decide which value should be written to database
                            //proposedValues[property] = proposedValue;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }

                return Context.Entry(entity).Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Get Filtered
        /// </summary>en
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync(TFilter filter)
        {
            var predicate = await new GenericTenantFilterPredicate<TEntity, TFilter>().GetPredicate(filter);

            var query = from ev in Get(predicate, q => q.OrderByDescending(d => d.Datestamp)) select ev;

            return await Task.Run(() => query);
        }

        /// <summary>
        /// Get Filtered Summary
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TSummary>> GetFilteredSummaryAsync(TFilter filter)
        {
            var predicate = await new GenericTenantFilterPredicate<TEntity, TFilter>().GetPredicate(filter);

            var query = from ev in Get(predicate, q => q.OrderByDescending(d => d.Datestamp)) select ev;

            return (IEnumerable<TSummary>)await Task.Run(() => query);
        }

        /// <summary>
        /// Get Summary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TSummary> GetSummaryAsync(Guid id)
        {
            var query = (IQueryable<TSummary>)DbSet;

            return (TSummary)await Task.Run(() => query);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<TSummary> GetSummaryAsync(string tenantId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<TSummary> GetSummaryAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used to filter and sort
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy?.Invoke(query).ToList() ?? query.ToList();
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id"></param>
        public virtual async Task DeleteAsync(Guid id, string lastModifiedBy)
        {
            var entity = DbSet.Find(id);

            await InsertDeleteAudit(entity, lastModifiedBy);

            if (entity != null)
            {
                DbSet.Remove(entity);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Return first
        /// </summary>
        /// <remarks>Primarily used in testing</remarks>
        /// <returns></returns>
        public virtual Task<TEntity> GetFirstAsync()
        {
            try
            {
                return DbSet.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Last (used for Testing)
        /// </summary>
        /// <returns></returns>
        public Task<TEntity> GetLastAsync()
        {
            try
            {
                return DbSet.LastOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Task<int> GetCountAsync()
        {
            return DbSet.CountAsync();
        }

        /// <summary>
        /// Insert audit
        /// </summary>
        /// <param name="originalEntity"></param>
        /// <returns></returns>
        public async Task InsertAudit(TEntity originalEntity, TEntity updatedEntity)
        {
            var audits = await GetAudits(originalEntity, updatedEntity);

            await DataContext.Audits.AddRangeAsync(audits);

            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Insert deletion audit
        /// </summary>
        /// <param name="originalEntity"></param>
        /// <param name="lastModifiedBy"></param>
        /// <returns></returns>
        public async Task InsertDeleteAudit(TEntity originalEntity, string lastModifiedBy)
        {
            var audit = new Audit()
            {
                Entity = typeof(TEntity).Name,
                EntityId = originalEntity.Id,
                Datestamp = DateTime.Now,
                FieldId = "DELETED",
                OldValue = "ACTIVE",
                NewValue = "DELETED",
                Created = DateTime.Now,
                LastModifiedBy = originalEntity.LastModifiedBy,
                CreatedBy = originalEntity.CreatedBy
            };

            await DataContext.Audits.AddAsync(audit);

            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Update audit
        /// </summary>
        /// <param name="originalEntity"></param>
        /// <param name="updatedEntity"></param>
        /// <returns></returns>
        public virtual async Task UpdateAudit(TEntity updatedEntity)
        {
            try
            {
                var originalEntity = await GetAsync(updatedEntity.Id);

                var audits = await GetAudits(originalEntity, updatedEntity);

                await DataContext.Audits.AddRangeAsync(audits);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Get audits
        /// </summary>
        /// <param name="originalEntity"></param>
        /// <param name="updatedEntity"></param>
        /// <returns></returns>
        protected async Task<List<Audit>> GetAudits(TEntity originalEntity, TEntity updatedEntity)
        {
            var audits = new List<Audit>();

            if (originalEntity == null)
            {
                originalEntity = new TEntity();
            }

            var differences = await GetDifferences(originalEntity, updatedEntity);

            foreach (var difference in differences)
            {
                var audit = new Audit()
                {
                    Entity = typeof(TEntity).Name,
                    EntityId = updatedEntity.Id,
                    Datestamp = updatedEntity.Datestamp,
                    FieldId = difference.PropertyName,
                    OldValue = difference.Object1Value,
                    NewValue = difference.Object2Value,
                    Created = DateTime.Now,
                    LastModifiedBy = updatedEntity.LastModifiedBy,
                    CreatedBy = updatedEntity.CreatedBy,
                    TenantId = updatedEntity.TenantId
                };

                audits.Add(audit);
            }

            return await Task.Run(() => audits);
        }

        private async Task<List<Difference>> GetDifferences(TEntity originalEntity, TEntity updatedEntity)
        {
            var compareLogic = new CompareLogic
            {
                Config =
                {
                    MaxDifferences = 20,
                    CompareChildren = true,
                    MaxMillisecondsDateDifference = 10000,
                    MembersToIgnore = new List<string>()
                    {
                        "Timestamp",
                    }
                }
            };
            var comparisonResult = compareLogic.Compare(originalEntity, updatedEntity);

            return await Task.Run(() => comparisonResult.Differences);
        }

        public virtual Task<TSummary> UpdateSummaryAsync(TSummary entity)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = (IQueryable<TEntity>)DbSet;

            return query.ToList();
        }

        public void UpdateDatabase()
        {
            UpdateDatabase(DataContext);
        }

        private static void UpdateDatabase(DataContext context)
        {
            if (!string.IsNullOrEmpty(DataContext.ConnectionString) && DataContext.ConnectionString != context.Database.GetConnectionString())
            {
                context.SetConnectionString(DataContext.ConnectionString);
            }
        }
    }
}