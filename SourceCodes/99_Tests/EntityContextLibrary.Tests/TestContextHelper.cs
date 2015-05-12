using System;
using System.Collections.Generic;

namespace Aliencube.EntityContextLibrary.Tests
{
    public static class ProductHelper
    {
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

        public static IList<Product> CreateProducts(IEnumerable<int> productIds)
        {
            var products = new List<Product>();
            foreach (var productId in productIds)
            {
                var product = CreateProduct(productId);
                products.Add(product);
            }
            return products;
        }

        public static Product CreateProduct(int productId)
        {
            var product = new Product()
                              {
                                  ProductId = productId,
                                  Name = String.Format("Product {0}", productId),
                                  Description = String.Format("Product Description {0}", productId),
                                  Price = productId,
                              };
            return product;
        }
    }

    public static class UserHelper
    {
        public static IList<User> CreateUsers(int count, int index = 0)
        {
            var users = new List<User>();
            for (var i = index; i < count + index; i++)
            {
                var user = CreateUser(i + 1);
                users.Add(user);
            }
            return users;
        }

        public static IList<User> CreateUsers(IEnumerable<int> userIds)
        {
            var users = new List<User>();
            foreach (var userId in userIds)
            {
                var user = CreateUser(userId);
                users.Add(user);
            }
            return users;
        }

        public static User CreateUser(int userId)
        {
            var user = new User()
                           {
                               UserId = userId,
                               Name = String.Format("User {0}", userId),
                               Email = String.Format("user-{0}@fakemail.com", userId),
                           };
            return user;
        }
    }
}