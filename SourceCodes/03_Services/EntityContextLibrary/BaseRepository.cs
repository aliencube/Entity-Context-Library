using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
        /// Initialises a new instance of the <c>BaseRepository</c> class.
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
        /// Gets the collection of entities queriable
        /// </summary>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Returns the collectioin of entities queriable.</returns>
        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return this._dbSet.Where(filter);
        }

        /// <summary>
        /// Gets the entire collection of entities queriable.
        /// </summary>
        /// <returns>Returns the entire collection of entities queriable.</returns>
        public virtual IQueryable<TEntity> Get()
        {
            return _dbSet;
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
        /// Adds the new entity.
        /// </summary>
        /// <param name="entity">Entity instance to add.</param>
        public virtual void Add(TEntity entity)
        {
            if (entity.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entity");
            }

            this._dbSet.Add(entity);
        }

        /// <summary>
        /// Adds the new list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to add.</param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Add(entity);
            }
        }

        /// <summary>
        /// Updates the existing entity.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        public virtual void Update(TEntity entity)
        {
            if (entity.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entity");
            }

            this._dbSet.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates the existing list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Update(entity);
            }
        }

        /// <summary>
        /// Deletes the entity corresponding to the entityId fro the DB set.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        public virtual void Delete(object entityId)
        {
            if (entityId == null || (int)entityId < 0)
            {
                throw new ArgumentOutOfRangeException("entityId");
            }

            var entity = this._dbSet.Find(entityId);
            this.Delete(entity);
        }

        /// <summary>
        /// Deletes the list of entities corresponding to the entityIds fro the DB set.
        /// </summary>
        /// <param name="entityIds">List of entityIds as primary keys.</param>
        public virtual void DeleteRange(IEnumerable<object> entityIds)
        {
            if (entityIds == null)
            {
                throw new ArgumentNullException("entityIds");
            }

            foreach (var entityId in entityIds)
            {
                this.Delete(entityId);
            }
        }

        /// <summary>
        /// Deletes the entity from the DB set.
        /// </summary>
        /// <param name="entity">Entity instance to delete.</param>
        public virtual void Delete(TEntity entity)
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
        }

        /// <summary>
        /// Deletes the list of entities from the DB set.
        /// </summary>
        /// <param name="entities">List of entity instances to delete.</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            foreach (var entity in entities)
            {
                this.Delete(entity);
            }
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