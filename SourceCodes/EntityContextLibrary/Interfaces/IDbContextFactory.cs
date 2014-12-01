﻿using System;
using System.Data.Entity;

namespace Aliencube.EntityContextLibrary.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>DbContextFactory</c> class.
    /// </summary>
    public interface IDbContextFactory : IDisposable
    {
        /// <summary>
        /// Gets the <c>DbContext</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContext</c> instance.</returns>
        [Obsolete("Use CreateContext() instead.")]
        DbContext Get();

        /// <summary>
        /// Creates the <c>DbContext</c> instance.
        /// </summary>
        /// <returns>Returns the <c>DbContext</c> instance.</returns>
        DbContext CreateContext();
    }
}