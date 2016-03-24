using System.Data.Entity;

namespace Aliencube.EntityContextLibrary.Tests.Models
{
    /// <summary>
    /// This represents the DB context entity for user.
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UserContext" /> class.
        /// </summary>
        public UserContext()
            : base("Name=UserContext")
        {
        }

        /// <summary>
        /// Gets or sets the collection of users.
        /// </summary>
        public IDbSet<User> Users { get; set; }
    }
}