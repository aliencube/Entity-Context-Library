using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Aliencube.EntityContextLibrary.Extensions;
using Aliencube.EntityContextLibrary.Interfaces;

using Microsoft.Data.Entity;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the factory entity for the <see cref="DbContext"/> instances.
    /// </summary>
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IEnumerable<DbContext> _dbContexts;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="DbContextFactory"/> class.
        /// </summary>
        /// <param name="dbContexts">List of the <see cref="DbContext"/> instances.</param>
        public DbContextFactory(params DbContext[] dbContexts)
            : this(dbContexts.ToList())
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DbContextFactory"/> class.
        /// </summary>
        /// <param name="dbContexts">List of the <see cref="DbContext"/> instances.</param>
        public DbContextFactory(IEnumerable<DbContext> dbContexts)
        {
            if (dbContexts.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(dbContexts));
            }

            this._dbContexts = dbContexts;
        }

        /// <summary>
        /// Gets the <see cref="DbContext"/> instance.
        /// </summary>
        /// <typeparam name="TContext">Type of database context inheriting the <see cref="DbContext"/> class.</typeparam>
        /// <returns>Returns the <see cref="DbContext"/> instance.</returns>
        public TContext GetDbContext<TContext>() where TContext : DbContext
        {
            var context = this._dbContexts.OfType<TContext>().SingleOrDefault();
            if (context == null)
            {
                throw new InvalidOperationException("DbContext not found");
            }

            return context;
        }

        /// <summary>
        /// Gets the <see cref="DbContext"/> instance.
        /// </summary>
        /// <param name="type">Type of database context inheriting the <see cref="DbContext"/> class.</param>
        /// <returns>Returns the <see cref="DbContext"/> instance.</returns>
        public DbContext GetDbContext(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var context = this._dbContexts.SingleOrDefault(p => p.GetType() == type);
            if (context == null)
            {
                throw new InvalidOperationException("DbContext not found");
            }

            return context;
        }

        /// <summary>
        /// Gets the list of the <see cref="DbContext"/> instances.
        /// </summary>
        /// <param name="types">List of database context types.</param>
        /// <returns>Returns the list of the <see cref="DbContext"/> instances.</returns>
        public IEnumerable<DbContext> GetDbContexts(IEnumerable<Type> types)
        {
            if (types.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(types));
            }

            if (types.Any(p => !p.GetTypeInfo().IsSubclassOf(typeof(DbContext))))
            {
                throw new ArgumentException("Invalid DbContext type", nameof(types));
            }

            var contexts = this._dbContexts.Where(p => types.Contains(p.GetType()));
            return contexts;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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
