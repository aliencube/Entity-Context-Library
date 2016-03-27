using System;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Aliencube.EntityContextLibrary.Tests.Models
{
    /// <summary>
    /// This represents the database context entity for product.
    /// </summary>
    public class ProductDbContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ProductDbContext"/> class.
        /// </summary>
        public ProductDbContext()
            : base()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ProductDbContext"/> class.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> instance.</param>
        /// <param name="options"><see cref="DbContextOptions{ProductDbContext}"/> instance.</param>
        public ProductDbContext(IServiceProvider serviceProvider, DbContextOptions<ProductDbContext> options)
            : base(serviceProvider, options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Product}"/> instance.
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}
