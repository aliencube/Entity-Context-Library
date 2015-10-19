using System.Data.Entity;

namespace Aliencube.EntityContextLibrary.Tests.Models
{
    /// <summary>
    /// This represents the DB context entity for product.
    /// </summary>
    public class ProductContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ProductContext" /> class.
        /// </summary>
        public ProductContext()
            : base("Name=ProductContext")
        {
        }

        /// <summary>
        /// Gets or sets the collection of the products.
        /// </summary>
        public IDbSet<Product> Products { get; set; }
    }
}