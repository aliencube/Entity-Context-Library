using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Aliencube.EntityContextLibrary.Extensions
{
    /// <summary>
    /// This represents the entity that contains extension methods for the <c>DbContext</c> class.
    /// </summary>
    public static partial class DbContextQueryExtensions
    {
        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static IEnumerable<TOutput> ExecuteStoreQuery<TOutput>(this DbContext context, string commandText, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = adapter.ObjectContext.ExecuteStoreQuery<TOutput>(commandText, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="executionOptions"><c>ExecutionOptions</c> object.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static IEnumerable<TOutput> ExecuteStoreQuery<TOutput>(this DbContext context, string commandText, ExecutionOptions executionOptions, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = adapter.ObjectContext.ExecuteStoreQuery<TOutput>(commandText, executionOptions, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="entitySetName">The entity set of the TResult type.</param>
        /// <param name="executionOptions"><c>ExecutionOptions</c> object.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static IEnumerable<TOutput> ExecuteStoreQuery<TOutput>(this DbContext context, string commandText, string entitySetName, ExecutionOptions executionOptions, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = adapter.ObjectContext.ExecuteStoreQuery<TOutput>(commandText, entitySetName, executionOptions, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="entitySetName">The entity set of the TResult type.</param>
        /// <param name="mergeOption">The <c>MergeOption</c> to use when executing the query.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static IEnumerable<TOutput> ExecuteStoreQuery<TOutput>(this DbContext context, string commandText, string entitySetName, MergeOption mergeOption, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = adapter.ObjectContext.ExecuteStoreQuery<TOutput>(commandText, entitySetName, mergeOption, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results asynchronously.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static async Task<IEnumerable<TOutput>> ExecuteStoreQueryAsync<TOutput>(this DbContext context, string commandText, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreQueryAsync<TOutput>(commandText, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results asynchronously.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="executionOptions"><c>ExecutionOptions</c> object.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static async Task<IEnumerable<TOutput>> ExecuteStoreQueryAsync<TOutput>(this DbContext context, string commandText, ExecutionOptions executionOptions, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreQueryAsync<TOutput>(commandText, executionOptions, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results asynchronously.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to observe while waiting for the task to complete.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static async Task<IEnumerable<TOutput>> ExecuteStoreQueryAsync<TOutput>(this DbContext context, string commandText, CancellationToken cancellationToken, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreQueryAsync<TOutput>(commandText, cancellationToken, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results asynchronously.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="executionOptions"><c>ExecutionOptions</c> object.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to observe while waiting for the task to complete.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static async Task<IEnumerable<TOutput>> ExecuteStoreQueryAsync<TOutput>(this DbContext context, string commandText, ExecutionOptions executionOptions, CancellationToken cancellationToken, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreQueryAsync<TOutput>(commandText, executionOptions, cancellationToken, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results asynchronously.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="entitySetName">The entity set of the TResult type.</param>
        /// <param name="executionOptions"><c>ExecutionOptions</c> object.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static async Task<IEnumerable<TOutput>> ExecuteStoreQueryAsync<TOutput>(this DbContext context, string commandText, string entitySetName, ExecutionOptions executionOptions, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreQueryAsync<TOutput>(commandText, entitySetName, executionOptions, parameters);
            return results;
        }

        /// <summary>
        /// Executes a query directly against the data source and returns a sequence of typed results asynchronously.
        /// </summary>
        /// <typeparam name="TOutput">Output type parameter.</typeparam>
        /// <param name="context"><c>DbContext</c> instance.</param>
        /// <param name="commandText">Query to run a stored procedure.</param>
        /// <param name="entitySetName">The entity set of the TResult type.</param>
        /// <param name="executionOptions"><c>ExecutionOptions</c> object.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to observe while waiting for the task to complete.</param>
        /// <param name="input">Input value.</param>
        /// <returns>Returns the list of <c>TOutput</c> objects.</returns>
        public static async Task<IEnumerable<TOutput>> ExecuteStoreQueryAsync<TOutput>(this DbContext context, string commandText, string entitySetName, ExecutionOptions executionOptions, CancellationToken cancellationToken, object input)
        {
            var parameters = ConvertHelper.ConvertToParameters(input);
            var adapter = (IObjectContextAdapter)context;
            var results = await adapter.ObjectContext.ExecuteStoreQueryAsync<TOutput>(commandText, entitySetName, executionOptions, cancellationToken, parameters);
            return results;
        }
    }
}