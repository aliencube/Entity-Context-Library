using System;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Models;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Aliencube.EntityContextLibrary.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for the <see cref="DbContextFactoryTest"/> class.
    /// </summary>
    public class DbContextFactoryFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="DbContextFactoryFixture"/> class.
        /// </summary>
        public DbContextFactoryFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddEntityFramework().AddRelational().AddInMemoryDatabase();

            var productDbContextBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            productDbContextBuilder.UseInMemoryDatabase();

            this.ProductDbContext = new ProductDbContext(serviceCollection.BuildServiceProvider(), productDbContextBuilder.Options);

            var userDbContextBuilder = new DbContextOptionsBuilder<UserDbContext>();
            userDbContextBuilder.UseInMemoryDatabase();

            this.UserDbContext = new UserDbContext(serviceCollection.BuildServiceProvider(), userDbContextBuilder.Options);

            this.DbContextFactory = new DbContextFactory(this.ProductDbContext, this.UserDbContext);
        }

        /// <summary>
        /// Gets the <see cref="ProductDbContext"/> instance.
        /// </summary>
        public ProductDbContext ProductDbContext { get; }

        /// <summary>
        /// Gets the <see cref="UserDbContext"/> instance.
        /// </summary>
        public UserDbContext UserDbContext { get; }

        /// <summary>
        /// Gets the <see cref="IDbContextFactory"/> instance.
        /// </summary>
        public IDbContextFactory DbContextFactory { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value indicating whether the instance is being disposed or not.</param>
        protected virtual void Dispose(bool disposing)
        {
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