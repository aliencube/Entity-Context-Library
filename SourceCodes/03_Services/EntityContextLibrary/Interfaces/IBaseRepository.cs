using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>BaseRepository</c> class.
    /// </summary>
    /// <typeparam name="TEntity">Entity model type.</typeparam>
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Gets the <c>DbContext</c> instance.
        /// </summary>
        DbContext Context { get; }

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
        /// <param name="entity">Entity instance to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void Add(TEntity entity, bool save = true);

        /// <summary>
        /// Adds the new entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity instance to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void AddAsync(TEntity entity, bool save = true);

        /// <summary>
        /// Adds the new list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void AddRange(IEnumerable<TEntity> entities, bool save = true);

        /// <summary>
        /// Adds the new list of entities asynchronously.
        /// </summary>
        /// <param name="entities">List of entity instances to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void AddRangeAsync(IEnumerable<TEntity> entities, bool save = true);

        /// <summary>
        /// Updates the existing entity.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void Update(TEntity entity, bool save = true);

        /// <summary>
        /// Updates the existing entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void UpdateAsync(TEntity entity, bool save = true);

        /// <summary>
        /// Updates the existing list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void UpdateRange(IEnumerable<TEntity> entities, bool save = true);

        /// <summary>
        /// Updates the existing list of entities asynchronously.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void UpdateRangeAsync(IEnumerable<TEntity> entities, bool save = true);

        /// <summary>
        /// Adds or updates entity.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void AddOrUpdate(TEntity entity, bool save = true);

        /// <summary>
        /// Adds or updates entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void AddOrUpdateAsync(TEntity entity, bool save = true);

        /// <summary>
        /// Adds or updates the existing list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void AddOrUpdateRange(IEnumerable<TEntity> entities, bool save = true);

        /// <summary>
        /// Adds or updates the existing list of entities asynchronously.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void AddOrUpdateRangeAsync(IEnumerable<TEntity> entities, bool save = true);

        /// <summary>
        /// Deletes the entity corresponding to the entityId fro the DB set.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void Delete(object entityId, bool save = true);

        /// <summary>
        /// Deletes the entity corresponding to the entityId fro the DB set asynchronously.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void DeleteAsync(object entityId, bool save = true);

        /// <summary>
        /// Deletes the list of entities corresponding to the entityIds fro the DB set.
        /// </summary>
        /// <param name="entityIds">List of entityIds as primary keys.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void DeleteRange(IEnumerable<object> entityIds, bool save = true);

        /// <summary>
        /// Deletes the list of entities corresponding to the entityIds fro the DB set asynchronously.
        /// </summary>
        /// <param name="entityIds">List of entityIds as primary keys.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void DeleteRangeAsync(IEnumerable<object> entityIds, bool save = true);

        /// <summary>
        /// Deletes the entity from the DB set.
        /// </summary>
        /// <param name="entity">Entity instance to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void Delete(TEntity entity, bool save = true);

        /// <summary>
        /// Deletes the entity from the DB set asynchronously.
        /// </summary>
        /// <param name="entity">Entity instance to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void DeleteAsync(TEntity entity, bool save = true);

        /// <summary>
        /// Deletes the list of entities from the DB set.
        /// </summary>
        /// <param name="entities">List of entity instances to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void DeleteRange(IEnumerable<TEntity> entities, bool save = true);

        /// <summary>
        /// Deletes the list of entities from the DB set asynchronously.
        /// </summary>
        /// <param name="entities">List of entity instances to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        void DeleteRangeAsync(IEnumerable<TEntity> entities, bool save = true);
    }
}