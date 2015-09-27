using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Aliencube.EntityContextLibrary.Extensions
{
    /// <summary>
    /// This represents the helper entity for data type conversion.
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// Converts the object to the <c>Dictionary{string, object}</c> type object.
        /// </summary>
        /// <param name="input">Data object.</param>
        /// <returns>Returns the <c>Dictionary{string, object}</c> type object.</returns>
        public static IDictionary<string, object> ConvertToDictionary(object input)
        {
            var dictionary = new Dictionary<string, object>();
            if (input == null)
            {
                return dictionary;
            }

            dictionary = input.GetType()
                              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                              .ToDictionary(pi => pi.Name, pi => pi.GetValue(input));
            return dictionary;
        }

        /// <summary>
        /// Converts the object to the <c>SqlParameter</c> array.
        /// </summary>
        /// <param name="input">Data object.</param>
        /// <returns>Returns the <c>SqlParameter</c> array.</returns>
        public static object[] ConvertToParameters(object input)
        {
            if (input == null)
            {
                return null;
            }

            var dictionary = ConvertToDictionary(input);
            if (dictionary == null || !dictionary.Any())
            {
                return null;
            }

            var parameters = dictionary.Select(kvp => new SqlParameter(kvp.Key, kvp.Value) as object);
            return parameters.ToArray();
        }

        /// <summary>
        /// Converts the <c>Dictionary{string, object}</c> object to the <c>SqlParameter</c> array.
        /// </summary>
        /// <param name="input"><c>Dictionary{string, object}</c> object.</param>
        /// <returns>Returns the <c>SqlParameter</c> array.</returns>
        public static object[] ConvertToParameters(IDictionary<string, object> input)
        {
            if (input == null || !input.Any())
            {
                return null;
            }

            var parameters = input.Select(kvp => new SqlParameter(kvp.Key, kvp.Value) as object);
            return parameters.ToArray();
        }
    }
}