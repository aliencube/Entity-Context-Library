using System;
using System.Data.Entity;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>UnitOfWork</c> class.
    /// </summary>
    /// <typeparam name="TContext"><c>DbContext</c> type instance.</typeparam>
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : DbContext
    {
        /// <summary>
        /// Begins database transactions.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Saves database changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Commits database transactions.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back database transactions.
        /// </summary>
        void Rollback();
    }
}