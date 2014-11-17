using Aliencube.EntityContextLibrary.Interfaces;
using System;
using System.Data.Entity;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the <c>DbContextFactoryBase</c> class which must be inherited.
    /// </summary>
    public class DbContextFactory<TContext> : IDbContextFactory where TContext : DbContext
    {
        private TContext _dbContext;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <c>ChangeNotificationContextFactory</c> class.
        /// </summary>
        private void InitialiseContext()
        {
            this._dbContext = Activator.CreateInstance<TContext>();
        }

        /// <summary>
        /// Gets the <c>DbContext</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContext</c> instance.</returns>
        public virtual DbContext Get()
        {
            if (this._dbContext == null)
            {
                this.InitialiseContext();
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