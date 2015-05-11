using System.Data.Entity.Migrations;

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
        }

        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}