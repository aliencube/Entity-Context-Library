using System;

using Aliencube.EntityContextLibrary.Interfaces;

using FluentAssertions;

using NUnit.Framework;

namespace Aliencube.EntityContextLibrary.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="DbContextFactory{TContext}" /> class.
    /// </summary>
    [TestFixture]
    public class DbContextFactoryTest
    {
        private IDbContextFactory _factory;

        /// <summary>
        /// Initialises all resources for tests.
        /// </summary>
        [SetUp]
        public void Init()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            this._factory = new DbContextFactory<ProductContext>();
        }

        /// <summary>
        /// Release all resources after tests.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            if (this._factory != null)
            {
                this._factory.Dispose();
            }
        }

        /// <summary>
        /// Tests whether the context factory returns DbContext with given type or not.
        /// </summary>
        /// <param name="expectedType">
        /// The expected type.
        /// </param>
        [Test]
        [TestCase(typeof(ProductContext))]
        public void ContextFactory_Should_Return_Context_Of_Given_Type(Type expectedType)
        {
            var context = this._factory.Context;
            context.Should().NotBeNull();
            context.Should().BeOfType(expectedType);
        }
    }
}