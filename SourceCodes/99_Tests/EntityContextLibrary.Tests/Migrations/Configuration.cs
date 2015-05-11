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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}