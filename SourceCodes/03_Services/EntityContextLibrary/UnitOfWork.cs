using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the entity for unit of work.
    /// </summary>
    /// <typeparam name="TContext"><c>DbContext</c> type instance.</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly IDbContextFactory _contextFactory;

        private TContext _dbContext;
        private ObjectContext _objectContext;
        private IDbTransaction _transaction;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <c>UnitOfWork</c> class.
        /// </summary>
        /// <param name="contextFactory"><c>DbContextFactory</c> instance.</param>
        public UnitOfWork(IDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            this._contextFactory = contextFactory;
            this._dbContext = this.GetDbContext(this._contextFactory);
            this._objectContext = this.GetObjectContext(this._dbContext);
            this.OpenDbConnection();
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

            var dbContext = contextFactory.CreateContext();
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
        /// Begins database transactions.
        /// </summary>
        public void BeginTransaction()
        {
            this.OpenDbConnection();
            this._transaction = this._objectContext.Connection.BeginTransaction();
        }

        /// <summary>
        /// Saves database changes.
        /// </summary>
        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Commits database transactions.
        /// </summary>
        public void Commit()
        {
            if (this._transaction == null)
            {
                return;
            }

            this.SaveChanges();
            this._transaction.Commit();
        }

        /// <summary>
        /// Rolls back database transactions.
        /// </summary>
        public void Rollback()
        {
            if (this._transaction == null)
            {
                return;
            }

            this._transaction.Rollback();

            // http://blog.oneunicorn.com/2011/04/03/rejecting-changes-to-entities-in-ef-4-1/

            foreach (var entry in this.Context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Deleted:
                        // Note - problem with deleted entities:
                        // When an entity is deleted its relationships to other entities are severed.
                        // This includes setting FKs to null for nullable FKs or marking the FKs as conceptually null (don't ask!)
                        // if the FK property is not nullable. You'll need to reset the FK property values to
                        // the values that they had previously in order to re-form the relationships.
                        // This may include FK properties in other entities for relationships where the
                        // deleted entity is the principal of the relationship–e.g. has the PK
                        // rather than the FK. I know this is a pain–it would be great if it could be made easier in the future, but for now it is what it is.
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
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

            if (this.GetConnectionState(this._objectContext) == ConnectionState.Open)
            {
                this._objectContext.Connection.Close();
            }

            //this.Context.Dispose();
            //this._contextFactory.Dispose();

            this._disposed = true;
        }
    }
}