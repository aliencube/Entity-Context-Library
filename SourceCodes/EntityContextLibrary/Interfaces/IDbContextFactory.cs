using System;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>DbContextFactoryBase</c> class.
    /// </summary>
    public interface IDbContextFactory : IDisposable
    {
        /// <summary>
        /// Gets the <c>DbContextBase</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContextBase</c> instance.</returns>
        DbContextBase Get();
    }
}