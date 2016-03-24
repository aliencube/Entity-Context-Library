using System.Data.Entity.Migrations;
using System.IO;
using System.Reflection;

namespace Aliencube.EntityContextLibrary.Tests.Migrations
{
    /// <summary>
    /// This represents the DB migration entity for the first migration.
    /// </summary>
    public partial class InitialCreate : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            this.CreateTable(
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

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.DropTable("dbo.Products");
        }

        private void SqlFromFile(string sqlFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(this.GetType(), sqlFileName);
            if (stream == null)
            {
                throw new FileNotFoundException("Could not find the embedded resource: " + sqlFileName);
            }

            var sqlToExecute = new StreamReader(stream).ReadToEnd();
            this.Sql(sqlToExecute);
        }
    }
}