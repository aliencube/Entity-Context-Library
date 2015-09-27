using System.ComponentModel.DataAnnotations;

namespace Aliencube.EntityContextLibrary.Tests
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}