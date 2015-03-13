using System;
using System.Data.Entity;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>DbContextFactory</c> class.
    /// </summary>
    public interface IDbContextFactory : IDisposable
    {
        /// <summary>
        /// Gets the <c>DbContext</c> instance.
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// Gets the type of the <c>DbContext</c> instance.
        /// </summary>
        Type DbContextType { get; }

        /// <summary>
        /// Creates the <c>DbContext</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContext</c> instance.</returns>
        [Obsolete("Use this.Context property instead.")]
        DbContext CreateContext();
    }
}