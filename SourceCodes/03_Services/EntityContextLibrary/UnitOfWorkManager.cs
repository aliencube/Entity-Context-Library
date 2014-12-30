using System;
using System.Data.Entity;
using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents an entity for the unit of work manager.
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly IDbContextFactory _contextFactory;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <c>UnitOfWorkManager</c> class.
        /// </summary>
        /// <param name="contextFactory"><c>DbContextFactory</c> instance.</param>
        public UnitOfWorkManager(IDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            this._contextFactory = contextFactory;
        }

        /// <summary>
        /// Creates a new <c>UnitOfWork</c> instance.
        /// </summary>
        /// <typeparam name="TContext"><c>DbContext</c> type instance.</typeparam>
        /// <returns>Returns a new <c>UnitOfWork</c> instance.</returns>
        public UnitOfWork<TContext> CreateInstance<TContext>()
            where TContext : DbContext
        {
            return new UnitOfWork<TContext>(this._contextFactory);
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

            //this._contextFactory.Dispose();

            this._disposed = true;
        }
    }
}