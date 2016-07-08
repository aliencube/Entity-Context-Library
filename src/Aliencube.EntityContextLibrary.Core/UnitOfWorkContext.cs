using System;
using System.Data;
using System.Data.Common;

using Aliencube.EntityContextLibrary.Interfaces;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the entity for unit of work in database transaction.
    /// </summary>
    /// <typeparam name="TContext">Database context type inheriting the <see cref="DbContext"/> class.</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly DbConnection _dbConnection;

        private IRelationalTransaction _transaction;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="contextFactory"><see cref="IDbContextFactory"/> instance.</param>
        public UnitOfWork(IDbContextFactory contextFactory)
        {
            this._dbContext = this.GetDbContext(contextFactory);
            this._dbConnection = this._dbContext.Database.GetDbConnection();
        }

        /// <summary>
        /// Begins the database transaction.
        /// </summary>
        public void BeginTransaction()
        {
            this.OpenDbConnection();
            this._transaction = this._dbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Commits the database transaction.
        /// </summary>
        public void Commit()
        {
            this._transaction.Commit();
        }

        /// <summary>
        /// Rolls back the database transaction.
        /// </summary>
        public void Rollback()
        {
            this._transaction.Rollback();
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
                this.CloseDbConnection();
            }

            this._disposed = true;
        }

        private TContext GetDbContext(IDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            var dbContext = contextFactory.GetDbContext<TContext>();
            var context = (TContext)Convert.ChangeType(dbContext, typeof(TContext));
            return context;
        }

        private void OpenDbConnection()
        {
            if (this._dbConnection.State == ConnectionState.Open)
            {
                return;
            }

            this._dbConnection.Open();
        }

        private void CloseDbConnection()
        {
            if (this._dbConnection.State == ConnectionState.Closed)
            {
                return;
            }

            this._dbConnection.Close();
        }
    }
}