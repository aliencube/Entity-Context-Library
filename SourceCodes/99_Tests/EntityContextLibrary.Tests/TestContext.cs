using System.Data.Entity;

namespace Aliencube.EntityContextLibrary.Tests
{
    public class ProductContext : DbContext
    {
        public ProductContext()
            : base("Name=ProductContext")
        {
        }

        public IDbSet<Product> Products { get; set; }
    }

    public class UserContext : DbContext
    {
        public UserContext()
            : base("Name=UserContext")
        {
        }

        public IDbSet<User> Users { get; set; }
    }
}