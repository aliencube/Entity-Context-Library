using System;
using System.Collections.Generic;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Models;

namespace Aliencube.EntityContextLibrary.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="UnitOfWorkManagerTest"/> class.
    /// </summary>
    public class UnitOfWorkManagerFixture : DbContextFactoryFixture
    {
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWorkManagerFixture"/> class.
        /// </summary>
        public UnitOfWorkManagerFixture()
            : base()
        {
            this.Types = new List<Type>() { typeof(ProductDbContext), typeof(UserDbContext) };

            this.UnifOfWorkManager = new UnitOfWorkManager(this.DbContextFactory);
        }

        /// <summary>
        /// Gets the list of <see cref="Type"/> objects.
        /// </summary>
        public List<Type> Types { get; }

        /// <summary>
        /// Gets the <see cref="IUnitOfWorkManager"/> instance.
        /// </summary>
        public IUnitOfWorkManager UnifOfWorkManager { get; }

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
