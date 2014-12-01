using Aliencube.EntityContextLibrary.Interfaces;
using System;
using System.Data.Entity;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the factory entity for <c>DbContext</c>.
    /// </summary>
    public class DbContextFactory<TContext> : IDbContextFactory where TContext : DbContext
    {
        private TContext _dbContext;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <c>DbContextFactory</c> class.
        /// </summary>
        private void Initialise()
        {
            this._dbContext = Activator.CreateInstance<TContext>();
        }

        /// <summary>
        /// Gets the <c>DbContext</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContext</c> instance.</returns>
        [Obsolete("Use CreateContext() instead.")]
        public virtual DbContext Get()
        {
            if (this._dbContext == null)
            {
                this.Initialise();
            }

            return this._dbContext;
        }

        /// <summary>
        /// Creates the <c>DbContext</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContext</c> instance.</returns>
        public virtual DbContext CreateContext()
        {
            if (this._dbContext == null)
            {
                this.Initialise();
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