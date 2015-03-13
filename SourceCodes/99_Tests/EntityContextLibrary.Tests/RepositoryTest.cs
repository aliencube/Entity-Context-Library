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
    public class RepositoryTest
    {
        private ProductContext _context;
        private IDbContextFactory _factory;
        private IBaseRepository<Product> _repository;

        [SetUp]
        public void Init()
        {
            this._context = Substitute.For<ProductContext>();
            this._factory = Substitute.For<IDbContextFactory>();
            this._factory.DbContextType.Returns(typeof(ProductContext));
        }

        [TearDown]
        public void Cleanup()
        {
            if (this._repository != null)
            {
                this._repository.Dispose();
            }

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
        [TestCase(3, 1, "Product 1")]
        public void GetProduct_GivenContext_ReturnProduct(int count, int productId, string name)
        {
            var products = ProductHelper.CreateProducts(count);
            var dbSet = Substitute.For<DbSet<Product>, IQueryable<Product>, IDbAsyncEnumerable<Product>>().SetupData(products);

            this._context.Products.Returns(dbSet);
            this._context.Set<Product>().Returns(dbSet);
            this._factory.Context.Returns(this._context);

            this._repository = new BaseRepository<Product>(this._factory);
            this._repository.Context.Should().BeSameAs(this._context);

            var product = this._repository.Get(p => p.ProductId == productId).SingleOrDefault();
            product.Should().NotBeNull();
            product.Name.ToLower().Should().Be(name.ToLower());

            product = this._repository.Get().SingleOrDefault(p => p.ProductId == productId);
            product.Should().NotBeNull();
            product.Name.ToLower().Should().Be(name.ToLower());
        }

        [Test]
        [TestCase(3, 4)]
        public void AddProduct_GivenContext_ProductAdded(int count, int productId)
        {
            var products = ProductHelper.CreateProducts(count);
            var dbSet = Substitute.For<DbSet<Product>, IQueryable<Product>, IDbAsyncEnumerable<Product>>().SetupData(products);

            this._context.Products.Returns(dbSet);
            this._context.Set<Product>().Returns(dbSet);
            this._factory.Context.Returns(this._context);

            this._repository = new BaseRepository<Product>(this._factory);
            this._repository.Context.Should().BeSameAs(this._context);

            var product = ProductHelper.CreateProduct(productId);
            this._repository.Add(product);

            var results = this._repository.Get();
            results.Should().HaveCount(count + 1);

            var result = this._repository.Get().SingleOrDefault(p => p.ProductId == productId);
            result.Should().NotBeNull();
            result.Price.Should().Be(productId);
        }

        [Test]
        [TestCase(3, 4, 5)]
        public void AddProducts_GivenContext_ProductsAdded(int count, params int[] productIds)
        {
            var products = ProductHelper.CreateProducts(count);
            var dbSet = Substitute.For<DbSet<Product>, IQueryable<Product>, IDbAsyncEnumerable<Product>>().SetupData(products);

            this._context.Products.Returns(dbSet);
            this._context.Set<Product>().Returns(dbSet);
            this._factory.Context.Returns(this._context);

            this._repository = new BaseRepository<Product>(this._factory);
            this._repository.Context.Should().BeSameAs(this._context);

            var newProducts = ProductHelper.CreateProducts(productIds);
            this._repository.AddRange(newProducts);

            var results = this._repository.Get();
            results.Should().NotBeNull();
            results.Should().HaveCount(count + productIds.Length);

            results = this._repository.Get(p => productIds.Contains(p.ProductId));
            results.Should().NotBeNull();
            results.Should().HaveCount(productIds.Length);
        }
    }
}