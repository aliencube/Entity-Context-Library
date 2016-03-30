using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Models;
using Aliencube.EntityContextLibrary.Tests.Fixtures;

using FluentAssertions;

using Microsoft.Data.Entity;

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

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public void Given_TContext_GetDbContext_ShouldReturn_Result()
        {
            var productDbContext = this._uowm.GetDbContext<ProductDbContext>();
            productDbContext.Should().BeOfType<ProductDbContext>();

            var userDbContext = this._uowm.GetDbContext<UserDbContext>();
            userDbContext.Should().BeOfType<UserDbContext>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_GetDbContext_ShouldThrow_Exception()
        {
            Action action = () => { var context = this._uowm.GetDbContext(null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="type">Type not inheriting the <see cref="DbContext"/> class.</param>
        [Theory]
        [InlineData(typeof(Action))]
        public void Given_InvalidType_GetDbContext_ShouldThrow_Exception(Type type)
        {
            Action action = () => { var context = this._uowm.GetDbContext(type); };
            action.ShouldThrow<InvalidOperationException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="type">Type inheriting the <see cref="DbContext"/> class.</param>
        [Theory]
        [InlineData(typeof(ProductDbContext))]
        [InlineData(typeof(UserDbContext))]
        public void Given_ValidType_GetDbContext_ShouldReturn_Result(Type type)
        {
            var context = this._uowm.GetDbContext(type);
            context.Should().BeOfType(type);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_GetDbContexts_ShouldThrow_Exception()
        {
            Action action = () => { var contexts = this._uowm.GetDbContexts(null); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var contexts = this._uowm.GetDbContexts(new List<Type>()); };
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="type1"><see cref="Type"/> either inheriting the <see cref="DbContext"/> class or not.</param>
        /// <param name="type2"><see cref="Type"/> either inheriting the <see cref="DbContext"/> class or not.</param>
        [Theory]
        [InlineData(typeof(ProductDbContext), typeof(Action))]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here.")]
        public void Given_InvalidTypes_GetDbContexts_ShouldThrow_Exception(Type type1, Type type2)
        {
            var types = new List<Type>(new[] { type1, type2 });

            Action action = () => { var contexts = this._uowm.GetDbContexts(types); };
            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="type1"><see cref="Type"/> inheriting the <see cref="DbContext"/> class.</param>
        /// <param name="type2"><see cref="Type"/> inheriting the <see cref="DbContext"/> class.</param>
        [Theory]
        [InlineData(typeof(ProductDbContext), typeof(UserDbContext))]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here.")]
        public void Given_ValidTypes_GetContexts_ShouldReturn_Result(Type type1, Type type2)
        {
            var types = new List<Type>(new[] { type1, type2 });

            var contexts = this._uowm.GetDbContexts(types).ToList();
            contexts.Should().HaveCount(types.Count());
            foreach (var type in types)
            {
                var context = contexts.SingleOrDefault(p => p.GetType() == type);
                context.Should().NotBeNull();
            }
        }
    }
}