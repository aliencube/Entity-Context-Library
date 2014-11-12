using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the <c>DbContextFactoryBase</c> class which must be inherited.
    /// </summary>
    public abstract class DbContextFactoryBase : IDbContextFactory
    {
        private bool _disposed;

        /// <summary>
        /// Gets the <c>DbContextBase</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContextBase</c> instance.</returns>
        public abstract DbContextBase Get();

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

            this._disposed = true;
        }
    }
}