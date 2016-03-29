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