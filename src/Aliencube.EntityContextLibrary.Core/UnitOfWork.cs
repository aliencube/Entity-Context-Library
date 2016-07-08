using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

using Aliencube.EntityContextLibrary.Extensions;
using Aliencube.EntityContextLibrary.Interfaces;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the entity for unit of work in database transaction.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEnumerable<DbContext> _dbContexts;
        private readonly IEnumerable<DbConnection> _dbConnections;

        private IEnumerable<IRelationalTransaction> _dbTransactions;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="contextFactory"><see cref="IDbContextFactory"/> instance.</param>
        /// <param name="type">Database context type inheriting the <see cref="DbContext"/> class.</param>
        public UnitOfWork(IDbContextFactory contextFactory, Type type)
            : this(contextFactory, new[] { type })
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="contextFactory"><see cref="IDbContextFactory"/> instance.</param>
        /// <param name="types">List of the database context types inheriting the <see cref="DbContext"/> class.</param>
        public UnitOfWork(IDbContextFactory contextFactory, IEnumerable<Type> types)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            if (types.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(types));
            }

            this._dbContexts = this.GetDbContexts(contextFactory, types);
            this._dbConnections = this.GetDbConnections();
        }

        /// <summary>
        /// Begins the database transaction.
        /// </summary>
        public void BeginTransaction()
        {
            this.OpenDbConnections();
            this._dbTransactions = this._dbContexts.Select(p => p.Database.BeginTransaction());
        }

        /// <summary>
        /// Commits the database transaction.
        /// </summary>
        public void Commit()
        {
            foreach (var dbTransaction in this._dbTransactions)
            {
                dbTransaction.Commit();
            }
        }

        /// <summary>
        /// Rolls back the database transaction.
        /// </summary>
        public void Rollback()
        {
            foreach (var dbTransaction in this._dbTransactions)
            {
                dbTransaction.Rollback();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value that indicates whether the instance is being disposed or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                this.CloseDbConnections();
            }

            this._disposed = true;
        }

        private IEnumerable<DbContext> GetDbContexts(IDbContextFactory contextFactory, IEnumerable<Type> types)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            if (types.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(types));
            }

            var contexts = contextFactory.GetDbContexts(types);
            return contexts;
        }

        private IEnumerable<DbConnection> GetDbConnections()
        {
            var connections = this._dbContexts.Select(p => p.Database.GetDbConnection());
            return connections;
        }

        private void OpenDbConnections()
        {
            foreach (var dbConnection in this._dbConnections.Where(dbConnection => dbConnection.State != ConnectionState.Open))
            {
                dbConnection.Open();
            }
        }

        private void CloseDbConnections()
        {
            foreach (var dbConnection in this._dbConnections.Where(dbConnection => dbConnection.State != ConnectionState.Closed))
            {
                dbConnection.Close();
            }
        }
    }
}