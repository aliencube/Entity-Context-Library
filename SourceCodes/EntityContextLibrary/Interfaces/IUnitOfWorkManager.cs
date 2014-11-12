using System;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>UnitOfWorkManager</c> class.
    /// </summary>
    public interface IUnitOfWorkManager : IDisposable
    {
        /// <summary>
        /// Creates a new <c>UnitOfWork</c> instance.
        /// </summary>
        /// <typeparam name="TContext"><c>DbContext</c> type instance.</typeparam>
        /// <returns>Returns a new <c>UnitOfWork</c> instance.</returns>
        UnitOfWork<TContext> CreateInstance<TContext>() where TContext : DbContextBase;
    }
}