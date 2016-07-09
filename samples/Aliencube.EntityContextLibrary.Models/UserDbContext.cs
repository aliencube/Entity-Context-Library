﻿using Aliencube.EntityContextLibrary.Extensions;
using Aliencube.EntityContextLibrary.Models.Mapping;

using Microsoft.EntityFrameworkCore;

namespace Aliencube.EntityContextLibrary.Models
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
        /// <param name="options"><see cref="DbContextOptions"/> instance.</param>
        public UserDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{User}"/> instance.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Called while entity models are created.
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/> instance.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().Map(new UserMap());
        }
    }
}