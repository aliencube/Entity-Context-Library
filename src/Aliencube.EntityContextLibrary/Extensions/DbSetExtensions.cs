using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Aliencube.EntityContextLibrary.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="DbSet{TEntity}"/> class.
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        /// Gets the queryable entity result set.
        /// </summary>
        /// <param name="dbSet"><see cref="DbSet{TEntity}"/> instance.</param>
        /// <param name="filter">Filter expression for query.</param>
        /// <typeparam name="TEntity">Database entity type.</typeparam>
        /// <returns>Returns the queryable entity result set.</returns>
        public static IQueryable<TEntity> Get<TEntity>(this DbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            if (dbSet == null)
            {
                throw new ArgumentNullException(nameof(dbSet));
            }

            if (filter == null)
            {
                return dbSet;
            }

            return dbSet.Where(filter);
        }

        /// <summary>
        /// Finds an entity within the given <see cref="DbSet{TEntity}"/> instance.
        /// </summary>
        /// <param name="dbSet"><see cref="DbSet{TEntity}"/> instance.</param>
        /// <param name="keyValues">List of key values to find entity.</param>
        /// <typeparam name="TEntity">Database entity type.</typeparam>
        /// <returns>Returns the <see cref="TEntity"/> found.</returns>
        /// <remarks>Reference: http://stackoverflow.com/questions/29030472/dbset-doesnt-have-a-find-method-in-ef7</remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static TEntity Find<TEntity>(this DbSet<TEntity> dbSet, params object[] keyValues) where TEntity : class
        {
            var context = dbSet.GetService<DbContext>();

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();

            var i = 0;
            foreach (var property in key.Properties)
            {
                entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValues[i]);
                i++;
            }

            var entry = entries.FirstOrDefault();
            if (entry != null)
            {
                // Return the local object if it exists.
                return entry.Entity;
            }

            // TODO: Build the real LINQ Expression
            // dbSet.Where(x => x.Id == keyValues[0]);
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = dbSet.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, key.Properties[0].Name),
                        Expression.Constant(keyValues[0])),
                    parameter));

            // Look in the database
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds an entity within the given <see cref="DbSet{TEntity}"/> instance asynchronously.
        /// </summary>
        /// <param name="dbSet"><see cref="DbSet{TEntity}"/> instance.</param>
        /// <param name="keyValues">List of key values to find entity.</param>
        /// <typeparam name="TEntity">Database entity type.</typeparam>
        /// <returns>Returns the <see cref="TEntity"/> found.</returns>
        /// <remarks>Reference: http://stackoverflow.com/questions/29030472/dbset-doesnt-have-a-find-method-in-ef7</remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static async Task<TEntity> FindAsync<TEntity>(this DbSet<TEntity> dbSet, params object[] keyValues) where TEntity : class
        {
            var context = dbSet.GetService<DbContext>();

            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();

            var entries = context.ChangeTracker.Entries<TEntity>();

            var i = 0;
            foreach (var property in key.Properties)
            {
                entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValues[i]);
                i++;
            }

            var entry = entries.FirstOrDefault();
            if (entry != null)
            {
                // Return the local object if it exists.
                return await Task.FromResult(entry.Entity).ConfigureAwait(false);
            }

            // TODO: Build the real LINQ Expression
            // dbSet.Where(x => x.Id == keyValues[0]);
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var query = dbSet.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, key.Properties[0].Name),
                        Expression.Constant(keyValues[0])),
                    parameter));

            // Look in the database
            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
