using System;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Fixtures;
using Aliencube.EntityContextLibrary.Tests.Models;

using FluentAssertions;

using Xunit;

namespace Aliencube.EntityContextLibrary.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="UnitOfWork{TContext}"/> class.
    /// </summary>
    public class UnitOfWorkContextTest : IClassFixture<UnitOfWorkContextFixture>
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWorkContextTest"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="UnitOfWorkContextFixture"/> instance.</param>
        public UnitOfWorkContextTest(UnitOfWorkContextFixture fixture)
        {
            this._dbContextFactory = fixture.DbContextFactory;
            this._uow = fixture.UnitOfWork;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParamemter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => { var constructor = new UnitOfWork<ProductDbContext>(null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        ///// <summary>
        ///// Tests whether the constructor should NOT throw an exception or not.
        ///// </summary>
        //[Fact]
        //public void Given_Parameters_Constructor_ShouldThrow_NoException()
        //{
        //    Action action = () => { var constructor = new UnitOfWork<ProductDbContext>(this._dbContextFactory); };
        //    action.ShouldNotThrow<Exception>();
        //}
    }
}
