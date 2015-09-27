using System.Data.Entity.Migrations;
using System.IO;
using System.Reflection;

namespace Aliencube.EntityContextLibrary.Tests.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId);

            this.SqlFromFile("AddProduct.sql");
            this.SqlFromFile("GetProduct.sql");
        }

        public override void Down()
        {
            DropTable("dbo.Products");
        }

        private void SqlFromFile(string sqlFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(GetType(), sqlFileName);
            if (stream == null)
            {
                throw new FileNotFoundException("Could not find the embedded resource: " + sqlFileName);
            }
            var sqlToExecute = new StreamReader(stream).ReadToEnd();
            Sql(sqlToExecute);
        }
    }
}