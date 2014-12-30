using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Aliencube.EntityContextLibrary.Interfaces;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Aliencube.EntityContextLibrary.Tests
{
    [TestFixture]
    public class DbContextFactoryTest
    {
        private TestContext _context;
        private IDbContextFactory _factory;

        [SetUp]
        public void Init()
        {
            this._context = Substitute.For<TestContext>();
            this._factory = Substitute.For<IDbContextFactory>();
        }

        [TearDown]
        public void Cleanup()
        {
            if (this._factory != null)
            {
                this._factory.Dispose();
            }

            if (this._context != null)
            {
                this._context.Dispose();
            }
        }

        [Test]
        [TestCase(3)]
        public void GetContext_GivenDetails_ReturnContext(int count)
        {
            var products = ProductHelper.CreateProducts(count);
            var dbSet = Substitute.For<DbSet<Product>, IQueryable<Product>, IDbAsyncEnumerable<Product>>().SetupData(products);

            this._context.Products.Returns(dbSet);
            this._factory.CreateContext().Returns(this._context);

            var context = this._factory.CreateContext();
            context.Should().NotBeNull();
        }
    }
}