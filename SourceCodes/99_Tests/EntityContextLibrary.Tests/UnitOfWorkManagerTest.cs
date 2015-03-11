using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Aliencube.EntityContextLibrary.Interfaces;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Aliencube.EntityContextLibrary.Tests
{
    using System.Collections.Generic;

    [TestFixture]
    public class UnitOfWorkManagerTest
    {
        private ProductContext _productContext;
        private UserContext _userContext;
        private IDbContextFactory _productContextFactory;
        private IDbContextFactory _userContextFactory;
        private IUnitOfWorkManager _uowm;

        [SetUp]
        public void Init()
        {
            this._productContext = Substitute.For<ProductContext>();
            this._userContext = Substitute.For<UserContext>();

            this._productContextFactory = Substitute.For<IDbContextFactory>();
            this._userContextFactory = Substitute.For<IDbContextFactory>();

            this._productContextFactory.DbContextType.Returns(typeof(ProductContext));
            this._userContextFactory.DbContextType.Returns(typeof(UserContext));

            this._uowm = new UnitOfWorkManager(this._productContextFactory, this._userContextFactory);
        }

        [TearDown]
        public void Cleanup()
        {
            if (this._uowm != null)
            {
                this._uowm.Dispose();
            }

            if (this._productContextFactory != null)
            {
                this._productContextFactory.Dispose();
            }

            if (this._userContextFactory != null)
            {
                this._userContextFactory.Dispose();
            }

            if (this._productContext != null)
            {
                this._productContext.Dispose();
            }

            if (this._userContext != null)
            {
                this._userContext.Dispose();
            }
        }

        [Test]
        [TestCase(3)]
        public void GetContext_GivenDetails_ReturnContext(int count)
        {
            var products = ProductHelper.CreateProducts(count);
            var productsAdded = ProductHelper.CreateProducts(count, count);
            var productDbSet = Substitute.For<DbSet<Product>, IQueryable<Product>, IDbAsyncEnumerable<Product>>().SetupData(products);
            productDbSet.AddRange(Arg.Any<IEnumerable<Product>>()).Returns(productsAdded);

            var users = UserHelper.CreateUsers(count);
            var usersAdded = UserHelper.CreateUsers(count, count);
            var userDbSet = Substitute.For<DbSet<User>, IQueryable<User>, IDbAsyncEnumerable<User>>().SetupData(users);
            userDbSet.AddRange(Arg.Any<IEnumerable<User>>()).Returns(usersAdded);

            this._productContext.Products.Returns(productDbSet);
            this._userContext.Users.Returns(userDbSet);

            this._productContextFactory.CreateContext().Returns(this._productContext);
            this._userContextFactory.CreateContext().Returns(this._userContext);

            var context = this._uowm.CreateInstance<ProductContext>();
            context.GetType().Should().BeOfType<ProductContext>();
        }
    }
}