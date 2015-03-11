using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents an entity for the unit of work manager.
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly IEnumerable<IDbContextFactory> _contextFactories;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <c>UnitOfWorkManager</c> class.
        /// </summary>
        /// <param name="contextFactories">List of the <c>DbContextFactory</c> instances.</param>
        public UnitOfWorkManager(params IDbContextFactory[] contextFactories)
        {
            if (contextFactories == null)
            {
                throw new ArgumentNullException("contextFactories");
            }

            if (contextFactories.Length == 0)
            {
                throw new InvalidOperationException("No parameter provided");
            }

            this._contextFactories = contextFactories;
        }

        /// <summary>
        /// Creates a new <c>UnitOfWork</c> instance.
        /// </summary>
        /// <typeparam name="TContext"><c>DbContext</c> type instance.</typeparam>
        /// <returns>Returns a new <c>UnitOfWork</c> instance.</returns>
        public UnitOfWork<TContext> CreateInstance<TContext>() where TContext : DbContext
        {
            var contextFactory = this._contextFactories
                                     .SingleOrDefault(p => p.DbContextType == typeof(TContext));
            if (contextFactory == null)
            {
                throw new InvalidOperationException("No DbContext found");
            }

            return new UnitOfWork<TContext>(contextFactory);
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