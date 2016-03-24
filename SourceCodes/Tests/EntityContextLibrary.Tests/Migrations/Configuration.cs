using System.Collections.Generic;
using System.Data.Entity.Migrations;

using Aliencube.EntityContextLibrary.Tests.Models;

namespace Aliencube.EntityContextLibrary.Tests.Migrations
{
    /// <summary>
    /// This represents the configuration entity for DB migration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ProductContext>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.ContextKey = "Aliencube.EntityContextLibrary.Tests.ProductContext";
        }

        /// <summary>
        /// Runs after upgrading to the latest migration to allow seed data to be updated.
        /// </summary>
        /// <param name="context">
        /// Context to be used for updating seed data.
        /// </param>
        protected override void Seed(ProductContext context)
        {
            var products = new List<Product>()
                           {
                               new Product() { Name = "Name 1", Description = "Description 1", Price = 100.00M },
                               new Product() { Name = "Name 2", Description = "Description 2", Price = 200.00M },
                           };

            products.ForEach(p => context.Products.AddOrUpdate(q => q.ProductId, p));
            context.SaveChanges();
        }
    }
}