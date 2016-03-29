using System;
using System.Collections.Generic;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Fixtures;

using FluentAssertions;

using Xunit;

namespace Aliencube.EntityContextLibrary.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="UnitOfWorkManager"/> class.
    /// </summary>
    public class UnitOfWorkManagerTest : IClassFixture<UnitOfWorkManagerFixture>
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly List<Type> _types;
        private readonly IUnitOfWorkManager _uowm;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWorkManagerTest"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="UnitOfWorkManagerFixture"/> instance.</param>
        public UnitOfWorkManagerTest(UnitOfWorkManagerFixture fixture)
        {
            this._dbContextFactory = fixture.DbContextFactory;
            this._types = fixture.Types;
            this._uowm = fixture.UnifOfWorkManager;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParamemter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => { var constructor = new UnitOfWorkManager(null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the constructor should NOT throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_Parameters_Constructor_ShouldThrow_NoException()
        {
            Action action = () => { var constructor = new UnitOfWorkManager(this._dbContextFactory); };
            action.ShouldNotThrow<Exception>();
        }
    }
}