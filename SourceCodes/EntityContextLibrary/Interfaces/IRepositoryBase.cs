using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>RepositoryBase</c> class.
    /// </summary>
    /// <typeparam name="TEntity">Entity model type.</typeparam>
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Gets the collection of entities queriable
        /// </summary>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Returns the collectioin of entities queriable.</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the entire collection of entities queriable.
        /// </summary>
        /// <returns>Returns the entire collection of entities queriable.</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        /// Gets the entity corresponding to the entityId.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <returns>Returns the entity corresponding to the entityId.</returns>
        TEntity Get(object entityId);

        /// <summary>
        /// Adds the new entity.
        /// </summary>
        /// <param name="entityToAdd">Entity instance to add.</param>
        void Add(TEntity entityToAdd);

        /// <summary>
        /// Updates the existing entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity instance to update.</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Deletes the entity corresponding to the entityId fro the DB set.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        void Delete(object entityId);

        /// <summary>
        /// Deletes the entity from the DB set.
        /// </summary>
        /// <param name="entityToDelete">Entity instance to delete.</param>
        void Delete(TEntity entityToDelete);
    }
}