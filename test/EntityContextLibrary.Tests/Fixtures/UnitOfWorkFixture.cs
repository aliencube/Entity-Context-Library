using System;
using System.Collections.Generic;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Models;

namespace Aliencube.EntityContextLibrary.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="UnitOfWorkTest"/> class.
    /// </summary>
    public class UnitOfWorkFixture : DbContextFactoryFixture
    {
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWorkFixture"/> class.
        /// </summary>
        public UnitOfWorkFixture()
            : base()
        {
            this.Types = new List<Type>() { typeof(ProductDbContext), typeof(UserDbContext) };
        }

        /// <summary>
        /// Gets the list of <see cref="Type"/> objects.
        /// </summary>
        public List<Type> Types { get; }

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
