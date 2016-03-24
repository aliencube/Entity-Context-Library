namespace Aliencube.EntityContextLibrary.Tests.Models
{
    /// <summary>
    /// This represents the model entity for user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }
    }
}