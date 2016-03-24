using System.Collections.Generic;
using System.Linq;

using Aliencube.EntityContextLibrary.Tests.Models;

namespace Aliencube.EntityContextLibrary.Tests.Helpers
{
    /// <summary>
    /// This represents the helper entity for users.
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// Creates the list of users.
        /// </summary>
        /// <param name="count">Number of users to return.</param>
        /// <param name="index">Index value for user.</param>
        /// <returns>
        /// Returns the list of users.
        /// </returns>
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

        /// <summary>
        /// Creates the list of users.
        /// </summary>
        /// <param name="userIds">The list of users Ids.</param>
        /// <returns>
        /// Returns the list of users.
        /// </returns>
        public static IList<User> CreateUsers(IEnumerable<int> userIds)
        {
            var users = userIds.Select(CreateUser).ToList();
            return users;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <returns>
        /// Returns the user created.
        /// </returns>
        public static User CreateUser(int userId)
        {
            var user = new User()
                           {
                               UserId = userId,
                               Name = string.Format("User {0}", userId),
                               Email = string.Format("user-{0}@fakemail.com", userId),
                           };
            return user;
        }
    }
}