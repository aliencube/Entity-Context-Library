using System;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Fixtures;
using Aliencube.EntityContextLibrary.Tests.Models;

using FluentAssertions;

using Xunit;

namespace Aliencube.EntityContextLibrary.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="DbContextFactory"/> class.
    /// </summary>
    public class DbContextFactoryTest : IClassFixture<DbContextFactoryFixture>
    {
        private readonly ProductDbContext _productDbContext;
        private readonly UserDbContext _userDbContext;
        private readonly IDbContextFactory _contextFactory;

        /// <summary>
        /// Initialises a new instance of the <see cref="DbContextFactoryTest"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="DbContextFactoryFixture"/> instance.</param>
        public DbContextFactoryTest(DbContextFactoryFixture fixture)
        {
            this._productDbContext = fixture.ProductDbContext;
            this._userDbContext = fixture.UserDbContext;
            this._contextFactory = fixture.DbContextFactory;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => { var constructor = new DbContextFactory(); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var constructor = new DbContextFactory(null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the constructor should NOT throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_Parameter_Constructor_ShouldThrow_NoException()
        {
            Action action = () => { var constructor = new DbContextFactory(this._productDbContext); };
            action.ShouldNotThrow<Exception>();
        }
    }
}
