using System;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>UnitOfWork</c> class.
    /// </summary>
    /// <typeparam name="TContext"><c>DbContext</c> type instance.</typeparam>
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : DbContextBase
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