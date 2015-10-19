using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        /// Gets the collection of entities queryable
        /// </summary>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Returns the collection of entities queryable.</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the collection of entities queryable
        /// </summary>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Returns the collection of entities queryable.</returns>
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the entire collection of entities queryable.
        /// </summary>
        /// <returns>Returns the entire collection of entities queryable.</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        /// Gets the entire collection of entities queryable.
        /// </summary>
        /// <returns>Returns the entire collection of entities queryable.</returns>
        Task<IQueryable<TEntity>> GetAsync();

        /// <summary>
        /// Gets the entity corresponding to the entityId.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <returns>Returns the entity corresponding to the entityId.</returns>
        TEntity Get(object entityId);

        /// <summary>
        /// Gets the entity corresponding to the entityId.
        /// </summary>
        /// <param name="entityId">EntityId as a primary key.</param>
        /// <returns>Returns the entity corresponding to the entityId.</returns>
        Task<TEntity> GetAsync(object entityId);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task AddAsync(TEntity entity, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task UpdateAsync(TEntity entity, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task AddOrUpdateAsync(TEntity entity, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task AddOrUpdateRangeAsync(IEnumerable<TEntity> entities, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task DeleteAsync(object entityId, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task DeleteRangeAsync(IEnumerable<object> entityIds, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task DeleteAsync(TEntity entity, bool save = true);

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
        /// <returns>Returns <see cref="Task" />.</returns>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool save = true);

        /// <summary>
        /// Execute stored procedure or direct SQL. This is mainly for the SELECT statements.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        IEnumerable<TOutput> ExecuteStoreQuery<TOutput>(string commandText, object input);

        /// <summary>
        /// Execute stored procedure or direct SQL asynchronously. This is mainly for the SELECT statements.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        Task<IEnumerable<TOutput>> ExecuteStoreQueryAsync<TOutput>(string commandText, object input);

        /// <summary>
        /// Execute stored procedure or direct SQL. This is mainly for the INSERT, UPDATE or DELETE statements.
        /// </summary>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <remarks>Make sure that this might return -1, if the stored procedure contains the <c>SET NOCOUNT ON</c> statement.</remarks>
        int ExecuteStoreCommand(string commandText, object input);

        /// <summary>
        /// Execute stored procedure or direct SQL asynchronously. This is mainly for the INSERT, UPDATE or DELETE statements.
        /// </summary>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the number of rows affected.</returns>
        /// <remarks>Make sure that this might return -1, if the stored procedure contains the <c>SET NOCOUNT ON</c> statement.</remarks>
        Task<int> ExecuteStoreCommandAsync(string commandText, object input);
    }
}