using System.ComponentModel.DataAnnotations;

namespace Aliencube.EntityContextLibrary.Tests.Models
{
    /// <summary>
    /// This represents the model entity for product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product Id.
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public decimal Price { get; set; }
    }
}