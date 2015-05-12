using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Aliencube.EntityContextLibrary.Extensions
{
    /// <summary>
    /// This represents the entity that contains extension methods for the <c>DbContext</c> class.
    /// </summary>
    public static partial class DbContextCommandExtensions
    {
        /// <summary>
        /// Executes a command against the database server that does not return a sequence of objects.
        /// </summary>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">List of input values.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteStoreCommand(this DbContext context, string commandText, IDictionary<string, object> input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);

            var adapter = (IObjectContextAdapter)context;
            var result = adapter.ObjectContext.ExecuteStoreCommand(commandText, parameters);
            return result;
        }

        /// <summary>
        /// Executes a command against the database server that does not return a sequence of objects.
        /// </summary>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="transactionalBehavior">Controls the creation of a transaction for this command.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">List of input values.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static int ExecuteStoreCommand(this DbContext context, TransactionalBehavior transactionalBehavior, string commandText, IDictionary<string, object> input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = adapter.ObjectContext.ExecuteStoreCommand(transactionalBehavior, commandText, parameters);
            return results;
        }

        /// <summary>
        /// Executes a command against the database server that does not return a sequence of objects asynchronously.
        /// </summary>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">List of input values.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static async Task<int> ExecuteStoreCommandAsync(this DbContext context, string commandText, IDictionary<string, object> input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreCommandAsync(commandText, parameters);
            return results;
        }

        /// <summary>
        /// Executes a command against the database server that does not return a sequence of objects asynchronously.
        /// </summary>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to observe while waiting for the task to complete.</param>
        /// <param name="input">List of input values.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static async Task<int> ExecuteStoreCommandAsync(this DbContext context, string commandText, CancellationToken cancellationToken, IDictionary<string, object> input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreCommandAsync(commandText, cancellationToken, parameters);
            return results;
        }

        /// <summary>
        /// Executes a command against the database server that does not return a sequence of objects asynchronously.
        /// </summary>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="transactionalBehavior">Controls the creation of a transaction for this command.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">List of input values.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static async Task<int> ExecuteStoreCommandAsync(this DbContext context, TransactionalBehavior transactionalBehavior, string commandText, IDictionary<string, object> input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreCommandAsync(transactionalBehavior, commandText, parameters);
            return results;
        }

        /// <summary>
        /// Executes a command against the database server that does not return a sequence of objects asynchronously.
        /// </summary>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="transactionalBehavior">Controls the creation of a transaction for this command.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to observe while waiting for the task to complete.</param>
        /// <param name="input">List of input values.</param>
        /// <returns>Returns the number of rows affected.</returns>
        public static async Task<int> ExecuteStoreCommandAsync(this DbContext context, TransactionalBehavior transactionalBehavior, string commandText, CancellationToken cancellationToken, IDictionary<string, object> input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreCommandAsync(transactionalBehavior, commandText, cancellationToken, parameters);
            return results;
        }
    }
}