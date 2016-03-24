using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Helpers;
using Aliencube.EntityContextLibrary.Tests.Models;

using FluentAssertions;

using NUnit.Framework;

namespace Aliencube.EntityContextLibrary.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="BaseRepository{TEntity}" /> class..
    /// </summary>
    [TestFixture]
    public class RepositoryTest
    {
        private ProductContext _context;
        private IDbContextFactory _factory;
        private IBaseRepository<Product> _repository;

        /// <summary>
        /// Initialises all resources for tests.
        /// </summary>
        [SetUp]
        public void Init()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            this._context = new ProductContext();
            this._factory = new DbContextFactory<ProductContext>();
            this._repository = new BaseRepository<Product>(this._factory);
        }

        /// <summary>
        /// Release all resources after tests.
        /// </summary>
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

        /// <summary>
        /// Tests whether a product is returned by given product Id or not.
        /// </summary>
        /// <param name="productId">The product Id.</param>
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Given_ProductId_Should_Return_Product(int productId)
        {
            var product = this._repository.Get(p => p.ProductId == productId).SingleOrDefault();
            product.Should().NotBeNull();

            product = this._repository.Get().SingleOrDefault(p => p.ProductId == productId);
            product.Should().NotBeNull();
        }

        /// <summary>
        /// Tests whether a product is added or not.
        /// </summary>
        /// <param name="name">The product name.</param>
        /// <param name="description">The product description.</param>
        /// <param name="price">The product price.</param>
        [Test]
        [TestCase("TEST Product", "TEST Description", 100.00)]
        public void Given_ProductDetails_Should_Add_Product_To_Repository(string name, string description, decimal price)
        {
            var product = new Product() { Name = name, Description = description, Price = price };
            this._repository.Add(product);

            var added = this._repository.Get().OrderByDescending(p => p.ProductId).First();
            added.Name.Should().Be(name);
            added.Description.Should().Be(description);
            added.Price.Should().Be(price);
        }

        /// <summary>
        /// Tests whether a product is added or not.
        /// </summary>
        /// <param name="name">The product name.</param>
        /// <param name="description">The product description.</param>
        /// <param name="price">The product price.</param>
        /// <returns>
        /// Returns <see cref="Task" />.
        /// </returns>
        [Test]
        [TestCase("TEST Product", "TEST Description", 100.00)]
        public async Task Given_ProductDetails_Should_Add_Product_To_RepositoryAsync(string name, string description, decimal price)
        {
            var product = new Product() { Name = name, Description = description, Price = price };
            await this._repository.AddAsync(product);

            var added = await this._repository.Get().OrderByDescending(p => p.ProductId).FirstAsync();
            added.Name.Should().Be(name);
            added.Description.Should().Be(description);
            added.Price.Should().Be(price);
        }

        /// <summary>
        /// Tests whether a number of products are added or not.
        /// </summary>
        /// <param name="count">Number of products to return.</param>
        /// <param name="index">Index value for product.</param>
        [Test]
        [TestCase(2, 11)]
        public void Given_ProductDetails_Should_Add_Products_To_Repository(int count, int index)
        {
            var products = ProductHelper.CreateProducts(count, index);
            this._repository.AddRange(products);

            var added = this._repository.Get().OrderByDescending(p => p.ProductId).Take(count).ToList();
            added.Last().Name.Should().Be(products.First().Name);
            added.Last().Description.Should().Be(products.First().Description);
            added.Last().Price.Should().Be(products.First().Price);
        }

        /// <summary>
        /// Tests whether a number of products are added or not.
        /// </summary>
        /// <param name="count">Number of products to return.</param>
        /// <param name="index">Index value for product.</param>
        /// <returns>
        /// Returns <see cref="Task" />.
        /// </returns>
        [Test]
        [TestCase(2, 11)]
        public async Task Given_ProductDetails_Should_Add_Products_To_RepositoryAsync(int count, int index)
        {
            var products = ProductHelper.CreateProducts(count, index);
            await this._repository.AddRangeAsync(products);

            var added = await this._repository.Get().OrderByDescending(p => p.ProductId).Take(count).ToListAsync();
            added.Last().Name.Should().Be(products.First().Name);
            added.Last().Description.Should().Be(products.First().Description);
            added.Last().Price.Should().Be(products.First().Price);
        }

        /// <summary>
        /// Tests whether a product is returned by executing stored procedure or not.
        /// </summary>
        /// <param name="productId">The product Id.</param>
        [Test]
        [TestCase(1)]
        public void Given_ProductId_Should_Run_StoredProcedure(int productId)
        {
            var results = this._repository.ExecuteStoreQuery<Product>("EXEC GetProduct @ProductId", new { ProductId = productId }).ToList();
            results.Should().HaveCount(1);
            results.Single().ProductId.Should().Be(productId);
        }

        /// <summary>
        /// Tests whether a product is returned by executing stored procedure or not.
        /// </summary>
        /// <param name="productId">The product Id.</param>
        /// <returns>
        /// Returns <see cref="Task" />.
        /// </returns>
        [Test]
        [TestCase(1)]
        public async Task Given_ProductId_Should_Run_StoredProcedureAsync(int productId)
        {
            var results = await this._repository.ExecuteStoreQueryAsync<Product>("EXEC GetProduct @ProductId", new { ProductId = productId });
            results.Single().ProductId.Should().Be(productId);
        }

        /// <summary>
        /// Tests whether a product is added by executing stored procedure or not.
        /// </summary>
        /// <param name="name">The product name.</param>
        /// <param name="description">The product description.</param>
        /// <param name="price">The product price.</param>
        [Test]
        [TestCase("TEST Product", "TEST Description", 100.00)]
        public void Given_ProductDetails_Should_Run_StoredProcedure(string name, string description, decimal price)
        {
            var result = this._repository.ExecuteStoreCommand("EXEC AddProduct @Name, @Description, @Price", new { Name = name, Description = description, Price = price });

            var added = this._repository.Get().OrderByDescending(p => p.ProductId).First();
            added.Name.Should().Be(name);
            added.Description.Should().Be(description);
            added.Price.Should().Be(price);
        }

        /// <summary>
        /// Tests whether a product is added by executing stored procedure or not.
        /// </summary>
        /// <param name="name">The product name.</param>
        /// <param name="description">The product description.</param>
        /// <param name="price">The product price.</param>
        /// <returns>
        /// Returns <see cref="Task" />.
        /// </returns>
        [Test]
        [TestCase("TEST Product", "TEST Description", 100.00)]
        public async Task Given_ProductDetails_Should_Run_StoredProcedureAsync(string name, string description, decimal price)
        {
            var result = await this._repository.ExecuteStoreCommandAsync("EXEC AddProduct @Name, @Description, @Price", new { Name = name, Description = description, Price = price });

            var added = await this._repository.Get().OrderByDescending(p => p.ProductId).FirstAsync();
            added.Name.Should().Be(name);
            added.Description.Should().Be(description);
            added.Price.Should().Be(price);
        }
    }
}