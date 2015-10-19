using System;
using System.Collections.Generic;
using System.Linq;
using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Models;

using FluentAssertions;
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
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            this._context = new ProductContext();
            this._factory = new DbContextFactory<ProductContext>();
            this._repository = new BaseRepository<Product>(this._factory);
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
        [TestCase(1)]
        public void GetProduct_GivenContext_ReturnProduct(int productId)
        {
            var product = this._repository.Get(p => p.ProductId == productId).SingleOrDefault();
            product.Should().NotBeNull();

            product = this._repository.Get().SingleOrDefault(p => p.ProductId == productId);
            product.Should().NotBeNull();
        }

        [Test]
        public void AddProduct_GivenContext_ProductAdded()
        {
            var productBeforeCount = this._repository.Get().Count();

            var product = new Product() { Name = "TEST Product", Description = "TEST Description", Price = 100.00M };
            this._repository.Add(product);

            var productAfterCount = this._repository.Get().Count();
            productAfterCount.Should().Be(productBeforeCount + 1);
        }

        [Test]
        public void AddProducts_GivenContext_ProductsAdded()
        {
            var productBeforeCount = this._repository.Get().Count();

            var products = new List<Product>()
                           {
                               new Product() { Name = "Name 11", Description = "Description 11", Price = 100.00M },
                               new Product() { Name = "Name 12", Description = "Description 12", Price = 200.00M },
                           };
            this._repository.AddRange(products);

            var productAfterCount = this._repository.Get().Count();

            productAfterCount.Should().Be(productBeforeCount + products.Count);
        }

        [Test]
        public void RunStoredQuery_GivenContext_ProductSelected()
        {
            var results = this._repository.ExecuteStoreQuery<Product>("EXEC GetProduct @ProductId", new { ProductId = 1 });
            results.Should().HaveCount(1);
        }

        [Test]
        public void RunStoredCommand_GivenContext_ProductAdded()
        {
            var productBeforeCount = this._repository.Get().Count();

            var result = this._repository.ExecuteStoreCommand("EXEC AddProduct @Name, @Description, @Price", new { Name = "Test Product", Description = "Test Description", Price = 50.00M });

            var productAfterCount = this._repository.Get().Count();

            productAfterCount.Should().Be(productBeforeCount + 1);
        }
    }
}