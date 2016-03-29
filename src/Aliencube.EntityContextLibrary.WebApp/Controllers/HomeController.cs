using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Models;

using Microsoft.AspNet.Mvc;

namespace Aliencube.EntityContextLibrary.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for /home.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IDbContextFactory _contextFactory;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="contextFactory"><see cref="IDbContextFactory"/> instance.</param>
        public HomeController(IDbContextFactory contextFactory)
        {
            this._contextFactory = contextFactory;
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Index()
        {
            var context = this._contextFactory.GetDbContext<ProductDbContext>();
            ViewBag.Context = context.GetType().Name;
            return this.View();
        }

        /// <summary>
        /// The about.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// The contact.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
