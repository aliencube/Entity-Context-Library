using System;
using Aliencube.EntityContextLibrary.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace Aliencube.EntityContextLibrary.Tests
{
    [TestFixture]
    public class DbContextFactoryTest
    {
        private IDbContextFactory _factory;

        [SetUp]
        public void Init()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            this._factory = new DbContextFactory<ProductContext>();
        }

        [TearDown]
        public void Cleanup()
        {
            if (this._factory != null)
            {
                this._factory.Dispose();
            }
        }

        [Test]
        [TestCase(typeof(ProductContext))]
        public void GetContext_GivenDetails_ReturnContext(Type expectedType)
        {
            var context = this._factory.Context;
            context.Should().NotBeNull();
            context.Should().BeOfType(expectedType);
        }
    }
}