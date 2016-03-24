using System;
using System.Data.Entity;

using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the factory entity for <c>DbContext</c>.
    /// </summary>
    /// <typeparam name="TContext">
    /// Type parameter of the context class inheriting <see cref="DbContext" />.
    /// </typeparam>
    public class DbContextFactory<TContext> : IDbContextFactory where TContext : DbContext
    {
        private TContext _dbContext;

        private bool _disposed;

        /// <summary>
        /// Gets the <c>DbContext</c> instance.
        /// </summary>
        public virtual DbContext Context
        {
            get
            {
                if (this._dbContext == null)
                {
                    this._dbContext = Activator.CreateInstance<TContext>();
                }

                return this._dbContext;
            }
        }

        /// <summary>
        /// Gets the type of the <c>DbContext</c> instance.
        /// </summary>
        public Type DbContextType
        {
            get
            {
                return typeof(TContext);
            }
        }

        /// <summary>
        /// Creates the <c>DbContext</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContext</c> instance.</returns>
        [Obsolete("Use this.Context property instead.")]
        public virtual DbContext CreateContext()
        {
            if (this._dbContext == null)
            {
                this._dbContext = Activator.CreateInstance<TContext>();
            }

            return this._dbContext;
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