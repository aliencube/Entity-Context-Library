using System.Data.Entity;

namespace Aliencube.EntityContextLibrary.Tests
{
    public class TestContext : DbContext
    {
        public TestContext()
            : base("Name=TestContext")
        {
        }

        public IDbSet<Product> Products { get; set; }
    }
}