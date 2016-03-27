using System;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Aliencube.EntityContextLibrary.Tests.Models
{
    /// <summary>
    /// This represents the database context entity for user.
    /// </summary>
    public class UserDbContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UserDbContext"/> class.
        /// </summary>
        public UserDbContext()
            : base()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="UserDbContext"/> class.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> instance.</param>
        /// <param name="options"><see cref="DbContextOptions{UserDbContext}"/> instance.</param>
        public UserDbContext(IServiceProvider serviceProvider, DbContextOptions<UserDbContext> options)
            : base(serviceProvider, options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{User}"/> instance.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
