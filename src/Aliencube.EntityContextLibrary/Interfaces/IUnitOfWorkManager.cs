using System;
using System.Collections.Generic;

using Microsoft.Data.Entity;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="UnitOfWorkManager"/> class.
    /// </summary>
    public interface IUnitOfWorkManager : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="DbContext"/> instance.
        /// </summary>
        /// <typeparam name="TContext">Type of database context inheriting the <see cref="DbContext"/> class.</typeparam>
        /// <returns>Returns the <see cref="DbContext"/> instance.</returns>
        TContext GetDbContext<TContext>() where TContext : DbContext;

        /// <summary>
        /// Gets the <see cref="DbContext"/> instance.
        /// </summary>
        /// <param name="type">Type of database context inheriting the <see cref="DbContext"/> class.</param>
        /// <returns>Returns the <see cref="DbContext"/> instance.</returns>
        DbContext GetDbContext(Type type);

        /// <summary>
        /// Gets the list of the <see cref="DbContext"/> instances.
        /// </summary>
        /// <param name="types">List of database context types.</param>
        /// <returns>Returns the list of the <see cref="DbContext"/> instances.</returns>
        IEnumerable<DbContext> GetDbContexts(IEnumerable<Type> types);

        /// <summary>
        /// Creates the <see cref="DbContext"/> instance with given type.
        /// </summary>
        /// <typeparam name="TContext">Database context type inheriting the <see cref="DbContext"/> class.</typeparam>
        /// <returns>Returns the <see cref="IUnitOfWork"/> instance.</returns>
        IUnitOfWork CreateInstance<TContext>() where TContext : DbContext;

        /// <summary>
        /// Creates the <see cref="DbContext"/> instance with given type.
        /// </summary>
        /// <param name="type">Database context type inheriting the <see cref="DbContext"/> class.</param>
        /// <returns>Returns the <see cref="IUnitOfWork"/> instance.</returns>
        IUnitOfWork CreateInstance(Type type);

        /// <summary>
        /// Creates the <see cref="DbContext"/> instance with given type.
        /// </summary>
        /// <param name="types">List of database context types inheriting the <see cref="DbContext"/> class.</param>
        /// <returns>Returns the <see cref="IUnitOfWork"/> instance.</returns>
        IUnitOfWork CreateInstance(IEnumerable<Type> types);
    }
}