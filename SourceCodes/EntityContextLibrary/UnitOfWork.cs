using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;

using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the entity for unit of work.
    /// </summary>
    /// <typeparam name="TContext"><c>DbContext</c> type instance.</typeparam>
    public partial class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly IDbContextFactory _contextFactory;

        private TContext _dbContext;
        private ObjectContext _objectContext;
        private IDbTransaction _transaction;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWork{TContext}" /> class.
        /// </summary>
        /// <param name="contextFactory"><see cref="DbContextFactory{TContext}" /> instance.</param>
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
        /// Saves database changes.
        /// </summary>
        /// <returns>
        /// Returns <see cref="Task" />.
        /// </returns>
        public async Task SaveChangesAsync()
        {
            await this.Context.SaveChangesAsync();
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
        /// Commits database transactions.
        /// </summary>
        /// <returns>
        /// Returns <see cref="Task" />.
        /// </returns>
        public async Task CommitAsync()
        {
            if (this._transaction == null)
            {
                return;
            }

            await this.SaveChangesAsync();
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
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            var connectionState = this.GetConnectionState(this._objectContext);
            if (connectionState == ConnectionState.Open)
            {
                this._objectContext.Connection.Close();
            }

            //this.Context.Dispose();
            //this._contextFactory.Dispose();

            this._disposed = true;
        }
    }
}