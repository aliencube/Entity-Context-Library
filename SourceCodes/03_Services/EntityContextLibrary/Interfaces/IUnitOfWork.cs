using System;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>UnitOfWork</c> class.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the type of the <c>DbContext</c> instance.
        /// </summary>
        Type DbContextType { get; }

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