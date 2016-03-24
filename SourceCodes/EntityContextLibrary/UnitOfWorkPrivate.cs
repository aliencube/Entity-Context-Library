using System;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the entity for unit of work.
    /// </summary>
    /// <typeparam name="TContext"><c>DbContext</c> type instance.</typeparam>
    public partial class UnitOfWork<TContext>
    {
        /// <summary>
        /// Gets the <c>DbContext</c> instance.
        /// </summary>
        private TContext Context
        {
            get
            {
                if (this._dbContext != null)
                {
                    return this._dbContext;
                }

                this._dbContext = this.GetDbContext(this._contextFactory);
                this._objectContext = this.GetObjectContext(this._dbContext);
                this.OpenDbConnection();

                return this._dbContext;
            }
        }

        /// <summary>
        /// Gets the <c>DbContext</c> from the <c>DbContextFactory</c> instance.
        /// </summary>
        /// <param name="contextFactory"><c>DbContextFactory</c> instance.</param>
        /// <returns>Returns the <c>DbContext</c> instance.</returns>
        private TContext GetDbContext(IDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            var dbContext = contextFactory.Context;
            var context = (TContext)Convert.ChangeType(dbContext, typeof(TContext));
            return context;
        }

        /// <summary>
        /// Gets the <c>ObjectContext</c> instance from <c>DbContext</c>.
        /// </summary>
        /// <param name="dbContext"><c>DbContext</c> instance.</param>
        /// <returns>Returns the <c>ObjectContext</c> instance.</returns>
        private ObjectContext GetObjectContext(TContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            var contextAdapter = (IObjectContextAdapter)dbContext;
            var objectContext = contextAdapter.ObjectContext;
            return objectContext;
        }

        /// <summary>
        /// Opens the database connection.
        /// </summary>
        private void OpenDbConnection()
        {
            if (this._objectContext.Connection.State == ConnectionState.Open)
            {
                return;
            }

            this._objectContext.Connection.Open();
        }

        /// <summary>
        /// Gets the database connection state.
        /// </summary>
        /// <param name="objectContext">ObjectContext instance.</param>
        /// <returns>Returns the database connection state.</returns>
        private ConnectionState GetConnectionState(ObjectContext objectContext)
        {
            if (objectContext == null)
            {
                return ConnectionState.Closed;
            }

            if (objectContext.Connection == null)
            {
                return ConnectionState.Closed;
            }

            return objectContext.Connection.State;
        }
    }
}