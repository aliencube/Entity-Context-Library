using System.Collections.Generic;
using System.Linq;

using Aliencube.EntityContextLibrary.Tests.Models;

namespace Aliencube.EntityContextLibrary.Tests.Helpers
{
    /// <summary>
    /// This represents the helper entity for products.
    /// </summary>
    public static class ProductHelper
    {
        /// <summary>
        /// Creates the list of products.
        /// </summary>
        /// <param name="count">Number of products to return.</param>
        /// <param name="index">Index value for product.</param>
        /// <returns>
        /// Returns the list of products.
        /// </returns>
        public static IList<Product> CreateProducts(int count, int index = 0)
        {
            var products = new List<Product>();
            for (var i = index; i < count + index; i++)
            {
                var product = CreateProduct(i + 1);
                products.Add(product);
            }

            return products;
        }

        /// <summary>
        /// Creates the list of products.
        /// </summary>
        /// <param name="productIds">The list of product Ids.</param>
        /// <returns>
        /// Returns the list of products.
        /// </returns>
        public static IList<Product> CreateProducts(IEnumerable<int> productIds)
        {
            var products = productIds.Select(CreateProduct).ToList();
            return products;
        }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="productId">The product Id.</param>
        /// <returns>
        /// Returns the product created.
        /// </returns>
        public static Product CreateProduct(int productId)
        {
            var product = new Product()
                              {
                                  ProductId = productId,
                                  Name = string.Format("Product {0}", productId),
                                  Description = string.Format("Product Description {0}", productId),
                                  Price = productId,
                              };
            return product;
        }
    }
}