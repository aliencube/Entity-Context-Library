using System;
using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Models;

using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Aliencube.EntityContextLibrary.Tests
{
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
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            this._productContext = new ProductContext();
            this._userContext = new UserContext();

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
        public void GetContext_GivenDetails_ReturnContext()
        {
            this._productContextFactory.Context.Returns(this._productContext);
            this._userContextFactory.Context.Returns(this._userContext);

            var puow = this._uowm.CreateInstance<ProductContext>();
            puow.DbContextType.Should().Be<ProductContext>();

            var uuow = this._uowm.CreateInstance<UserContext>();
            uuow.DbContextType.Should().Be<UserContext>();
        }
    }
}