using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace Aliencube.EntityContextLibrary.Tests.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Aliencube.EntityContextLibrary.Tests.ProductContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Aliencube.EntityContextLibrary.Tests.ProductContext";
        }

        protected override void Seed(Aliencube.EntityContextLibrary.Tests.ProductContext context)
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