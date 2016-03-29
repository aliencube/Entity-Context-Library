using System;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="UnitOfWork{TEntity}"/> class.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begins the database transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits the database transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back the database transaction.
        /// </summary>
        void Rollback();
    }
}