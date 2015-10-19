using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Aliencube.EntityContextLibrary.Extensions;
using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the <c>BaseRepository</c> class that must be inherited.
    /// </summary>
    /// <typeparam name="TEntity">Entity model class type.</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IDbContextFactory _contextFactory;
        private readonly IDbSet<TEntity> _dbSet;

        private DbContext _context;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="BaseRepository{TEntity}" /> class.
        /// </summary>
        /// <param name="contextFactory"><c>DbContextFactory</c> instance.</param>
        public BaseRepository(IDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            this._contextFactory = contextFactory;
            this.Context.Configuration.ProxyCreationEnabled = false;
            this._dbSet = this.Context.Set<TEntity>();
        }

        /// <summary>
        /// Gets the <c>DbContext</c> instance.
        /// </summary>
        public DbContext Context
        {
            get
            {
                if (this._context == null)
                {
                    this._context = this._contextFactory.Context;
                }

                return this._context;
            }
        }

        /// <summary>
        /// Gets the collection of entities queryable
        /// </summary>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Returns the collection of entities queryable.</returns>
        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return this._dbSet.Where(filter);
        }

        /// <summary>
        /// Gets the collection of entities queryable
        /// </summary>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Returns the collection of entities queryable.</returns>
        public virtual async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Task.FromResult(this._dbSet.Where(filter));
        }

        /// <summary>
        /// Gets the entire collection of entities queryable.
        /// </summary>
        /// <returns>Returns the entire collection of entities queryable.</returns>
        public virtual IQueryable<TEntity> Get()
        {
            return this._dbSet;
        }

        /// <summary>
        /// Gets the entire collection of entities queryable.
        /// </summary>
        /// <returns>Returns the entire collection of entities queryable.</returns>
        public virtual async Task<IQueryable<TEntity>> GetAsync()
        {
            return await Task.FromResult(this._dbSet);
        }

        /// <summary>
        /// Gets the entity corresponding to the entityId.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <returns>Returns the entity corresponding to the entityId.</returns>
        public virtual TEntity Get(object entityId)
        {
            if (entityId == null || (int)entityId < 0)
            {
                throw new ArgumentOutOfRangeException("entityId");
            }

            return this._dbSet.Find(entityId);
        }

        /// <summary>
        /// Gets the entity corresponding to the entityId.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <returns>Returns the entity corresponding to the entityId.</returns>
        public virtual async Task<TEntity> GetAsync(object entityId)
        {
            if (entityId == null || (int)entityId < 0)
            {
                throw new ArgumentOutOfRangeException("entityId");
            }

            return await Task.FromResult(this._dbSet.Find(entityId));
        }

        /// <summary>
        /// Adds the new entity.
        /// </summary>
        /// <param name="entity">Entity instance to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void Add(TEntity entity, bool save = true)
        {
            if (entity.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entity");
            }

            this._dbSet.Add(entity);

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds the new entity.
        /// </summary>
        /// <param name="entity">Entity instance to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task AddAsync(TEntity entity, bool save = true)
        {
            this.Add(entity, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Adds the new list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void AddRange(IEnumerable<TEntity> entities, bool save = true)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Add(entity, false);
            }

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds the new list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, bool save = true)
        {
            this.AddRange(entities, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Updates the existing entity.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void Update(TEntity entity, bool save = true)
        {
            if (entity.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entity");
            }

            this._dbSet.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the existing entity.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task UpdateAsync(TEntity entity, bool save = true)
        {
            this.Update(entity, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Updates the existing list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool save = true)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Update(entity, false);
            }

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the existing list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool save = true)
        {
            this.UpdateRange(entities, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Adds or updates entity.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void AddOrUpdate(TEntity entity, bool save = true)
        {
            if (entity.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entity");
            }

            this._dbSet.AddOrUpdate(entity);

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds or updates entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task AddOrUpdateAsync(TEntity entity, bool save = true)
        {
            this.AddOrUpdate(entity, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Adds or updates the existing list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void AddOrUpdateRange(IEnumerable<TEntity> entities, bool save = true)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.AddOrUpdate(entity, false);
            }

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds or updates the existing list of entities asynchronously.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task AddOrUpdateRangeAsync(IEnumerable<TEntity> entities, bool save = true)
        {
            this.AddOrUpdateRange(entities, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes the entity corresponding to the entityId fro the DB set.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void Delete(object entityId, bool save = true)
        {
            if (entityId == null || (int)entityId < 0)
            {
                throw new ArgumentOutOfRangeException("entityId");
            }

            var entity = this.Get(entityId);
            this.Delete(entity, false);

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the entity corresponding to the entityId fro the DB set.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task DeleteAsync(object entityId, bool save = true)
        {
            this.Delete(entityId, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes the list of entities corresponding to the entityIds fro the DB set.
        /// </summary>
        /// <param name="entityIds">List of entityIds as primary keys.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void DeleteRange(IEnumerable<object> entityIds, bool save = true)
        {
            if (entityIds == null)
            {
                throw new ArgumentNullException("entityIds");
            }

            foreach (var entityId in entityIds)
            {
                this.Delete(entityId, false);
            }

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the list of entities corresponding to the entityIds fro the DB set.
        /// </summary>
        /// <param name="entityIds">List of entityIds as primary keys.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task DeleteRangeAsync(IEnumerable<object> entityIds, bool save = true)
        {
            this.DeleteRange(entityIds, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes the entity from the DB set.
        /// </summary>
        /// <param name="entity">Entity instance to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void Delete(TEntity entity, bool save = true)
        {
            if (entity.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entity");
            }

            if (this.Context.Entry(entity).State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }

            this._dbSet.Remove(entity);

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the entity from the DB set.
        /// </summary>
        /// <param name="entity">Entity instance to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task DeleteAsync(TEntity entity, bool save = true)
        {
            this.Delete(entity, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes the list of entities from the DB set.
        /// </summary>
        /// <param name="entities">List of entity instances to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool save = true)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Delete(entity, false);
            }

            if (save)
            {
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the list of entities from the DB set.
        /// </summary>
        /// <param name="entities">List of entity instances to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        /// <returns>Returns <see cref="Task" />.</returns>
        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool save = true)
        {
            this.DeleteRange(entities, false);

            if (save)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Execute stored procedure or direct SQL. This is mainly for the SELECT statements.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public virtual IEnumerable<TOutput> ExecuteStoreQuery<TOutput>(string commandText, object input)
        {
            var results = this.Context.ExecuteStoreQuery<TOutput>(commandText, input);
            return results;
        }

        /// <summary>
        /// Execute stored procedure or direct SQL asynchronously. This is mainly for the SELECT statements.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public virtual async Task<IEnumerable<TOutput>> ExecuteStoreQueryAsync<TOutput>(string commandText, object input)
        {
            var results = await this.Context.ExecuteStoreQueryAsync<TOutput>(commandText, input);
            return results;
        }

        /// <summary>
        /// Execute stored procedure or direct SQL. This is mainly for the INSERT, UPDATE or DELETE statements.
        /// </summary>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <remarks>Make sure that this might return -1, if the stored procedure contains the <c>SET NOCOUNT ON</c> statement.</remarks>
        public virtual int ExecuteStoreCommand(string commandText, object input)
        {
            var result = this.Context.ExecuteStoreCommand(commandText, input);
            return result;
        }

        /// <summary>
        /// Execute stored procedure or direct SQL asynchronously. This is mainly for the INSERT, UPDATE or DELETE statements.
        /// </summary>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <remarks>Make sure that this might return -1, if the stored procedure contains the <c>SET NOCOUNT ON</c> statement.</remarks>
        public virtual async Task<int> ExecuteStoreCommandAsync(string commandText, object input)
        {
            var result = await this.Context.ExecuteStoreCommandAsync(commandText, input);
            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}