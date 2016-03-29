using System;
using System.Collections.Generic;
using System.Linq;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Fixtures;

using FluentAssertions;

using Xunit;

namespace Aliencube.EntityContextLibrary.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="UnitOfWork"/> class.
    /// </summary>
    public class UnitOfWorkTest : IClassFixture<UnitOfWorkFixture>
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly List<Type> _types;
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initialises a new instance of the <see cref="UnitOfWorkTest"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="UnitOfWorkFixture"/> instance.</param>
        public UnitOfWorkTest(UnitOfWorkFixture fixture)
        {
            this._dbContextFactory = fixture.DbContextFactory;
            this._types = fixture.Types;
            this._uow = fixture.UnitOfWork;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParamemter_Constructor_ShouldThrow_Exception()
        {
            Action action = () => { var constructor = new UnitOfWork(null, this._types.First()); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var constructor = new UnitOfWork(this._dbContextFactory, (Type)null); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var constructor = new UnitOfWork(this._dbContextFactory, (IEnumerable<Type>)null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the constructor should NOT throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_Parameters_Constructor_ShouldThrow_NoException()
        {
            Action action = () => { var constructor = new UnitOfWork(this._dbContextFactory, this._types.First()); };
            action.ShouldNotThrow<Exception>();

            action = () => { var constructor = new UnitOfWork(this._dbContextFactory, this._types); };
            action.ShouldNotThrow<Exception>();
        }
    }
}