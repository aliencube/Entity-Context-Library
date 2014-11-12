using Aliencube.EntityContextLibrary.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the <c>RepositoryBase</c> class that must be inherited.
    /// </summary>
    /// <typeparam name="TEntity">Entity model class type.</typeparam>
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly IDbContextFactory _contextFactory;
        private readonly IDbSet<TEntity> _dbSet;

        private DbContext _context;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <c>RepositoryBase</c> class.
        /// </summary>
        /// <param name="contextFactory"><c>DbContextFactory</c> instance.</param>
        protected RepositoryBase(IDbContextFactory contextFactory)
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
        /// Gets the DbContext instance.
        /// </summary>
        protected DbContext Context
        {
            get
            {
                if (this._context == null)
                {
                    this._context = this._contextFactory.Get();
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
        /// <param name="entityToAdd">Entity instance to add.</param>
        public virtual void Add(TEntity entityToAdd)
        {
            if (entityToAdd.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entityToAdd");
            }

            this._dbSet.Add(entityToAdd);
        }

        /// <summary>
        /// Updates the existing entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity instance to update.</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entityToUpdate");
            }

            this._dbSet.Attach(entityToUpdate);
            this.Context.Entry(entityToUpdate).State = EntityState.Modified;
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

            var entityToDelete = this._dbSet.Find(entityId);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes the entity from the DB set.
        /// </summary>
        /// <param name="entityToDelete">Entity instance to delete.</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete.Equals(default(TEntity)))
            {
                throw new ArgumentNullException("entityToDelete");
            }

            if (this.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this._dbSet.Attach(entityToDelete);
            }

            this._dbSet.Remove(entityToDelete);
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