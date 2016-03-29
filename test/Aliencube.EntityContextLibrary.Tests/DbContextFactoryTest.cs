using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Models;
using Aliencube.EntityContextLibrary.Tests.Fixtures;

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

            action = () => { var constructor = new DbContextFactory(this._userDbContext); };
            action.ShouldNotThrow<Exception>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public void Given_TContext_GetDbContext_ShouldReturn_Result()
        {
            var context = this._contextFactory.GetDbContext<ProductDbContext>();
            context.Should().BeOfType<ProductDbContext>();

            context = this._contextFactory.GetDbContext<UserDbContext>();
            context.Should().BeOfType<UserDbContext>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_GetDbContext_ShouldThrow_Exception()
        {
            Action action = () => { var context = this._contextFactory.GetDbContext(null); };
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
            Action action = () => { var context = this._contextFactory.GetDbContext(type); };
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
            var context = this._contextFactory.GetDbContext(type);
            context.Should().BeOfType(type);
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_GetDbContexts_ShouldThrow_Exception()
        {
            Action action = () => { var contexts = this._contextFactory.GetDbContexts(null); };
            action.ShouldThrow<ArgumentNullException>();

            action = () => { var contexts = this._contextFactory.GetDbContexts(new List<Type>()); };
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

            Action action = () => { var contexts = this._contextFactory.GetDbContexts(types); };
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

            var contexts = this._contextFactory.GetDbContexts(types).ToList();
            contexts.Should().HaveCount(types.Count());
            foreach (var type in types)
            {
                var context = contexts.SingleOrDefault(p => p.GetType() == type);
                context.Should().NotBeNull();
            }
        }
    }
}