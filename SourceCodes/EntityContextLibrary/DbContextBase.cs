using System.Data.Entity;

namespace Aliencube.EntityContextLibrary
{
    /// <summary>
    /// This represents the <c>DbContextBase</c> class which must be inherited.
    /// </summary>
    public abstract class DbContextBase : DbContext
    {
        /// <summary>
        /// Initialises a new instance of the <c>DbContextBase</c> class.
        /// </summary>
        /// <param name="contextName"><c>DbContext</c> name.</param>
        protected DbContextBase(string contextName)
            : base(contextName)
        {
        }
    }
}