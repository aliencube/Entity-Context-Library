using System;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Tests.Models;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace Aliencube.EntityContextLibrary.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="UnitOfWorkManager" /> class.
    /// </summary>
    [TestFixture]
    public class UnitOfWorkManagerTest
    {
        private ProductContext _productContext;
        private UserContext _userContext;
        private Mock<IDbContextFactory> _productContextFactory;
        private Mock<IDbContextFactory> _userContextFactory;
        private IUnitOfWorkManager _uowm;

        /// <summary>
        /// Initialises all resources for tests.
        /// </summary>
        [SetUp]
        public void Init()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            this._productContext = new ProductContext();
            this._userContext = new UserContext();

            this._productContextFactory = new Mock<IDbContextFactory>();
            this._userContextFactory = new Mock<IDbContextFactory>();

            this._productContextFactory.Setup(p => p.DbContextType).Returns(typeof(ProductContext));
            this._userContextFactory.Setup(p => p.DbContextType).Returns(typeof(UserContext));

            this._uowm = new UnitOfWorkManager(this._productContextFactory.Object, this._userContextFactory.Object);
        }

        /// <summary>
        /// Release all resources after tests.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            if (this._uowm != null)
            {
                this._uowm.Dispose();
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

        /// <summary>
        /// Tests whether the unit of work manager returns correct DbContext or not.
        /// </summary>
        [Test]
        public void UnitOfWorkManager_Should_Return_DbContext_Of_Type()
        {
            this._productContextFactory.Setup(p => p.Context).Returns(this._productContext);
            this._userContextFactory.Setup(p => p.Context).Returns(this._userContext);

            var puow = this._uowm.CreateInstance<ProductContext>();
            puow.DbContextType.Should().Be<ProductContext>();

            var uuow = this._uowm.CreateInstance<UserContext>();
            uuow.DbContextType.Should().Be<UserContext>();
        }
    }
}