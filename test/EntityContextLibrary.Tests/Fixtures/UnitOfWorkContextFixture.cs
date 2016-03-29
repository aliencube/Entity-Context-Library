using Aliencube.EntityContextLibrary.Interfaces;

namespace Aliencube.EntityContextLibrary.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="UnitOfWorkContextTest"/> class.
    /// </summary>
    public class UnitOfWorkContextFixture : DbContextFactoryFixture
    {
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWorkContextFixture"/> class.
        /// </summary>
        public UnitOfWorkContextFixture()
            : base()
        {
        }

        /// <summary>
        /// Gets the <see cref="IUnitOfWork"/> instance.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value indicating whether the instance is being disposed or not.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
            }

            this._disposed = true;
        }
    }
}