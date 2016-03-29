using System;

using Aliencube.EntityContextLibrary.Interfaces;
using Aliencube.EntityContextLibrary.Models;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aliencube.EntityContextLibrary.WebApp
{
    /// <summary>
    /// This represents the main entry point of the web API application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env"><see cref="IHostingEnvironment"/> instance.</param>
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets or sets the <see cref="IConfigurationRoot"/> instance.
        /// </summary>
        public IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Defines the main entry point of the web API application.
        /// </summary>
        /// <param name="args">List of arguments.</param>
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        /// <summary>
        /// Configures services including dependencies.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
        /// <returns>Returns the <see cref="IServiceProvider"/> instance.</returns>
        /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddEntityFramework()
                    .AddRelational()
                    .AddInMemoryDatabase()
                    .AddDbContext<ProductDbContext>(o => o.UseInMemoryDatabase())
                    .AddDbContext<UserDbContext>(o => o.UseInMemoryDatabase());

            var builder = new ContainerBuilder();
            builder.Register(c => new DbContextFactory(c.Resolve<ProductDbContext>(), c.Resolve<UserDbContext>()))
                   .As<IDbContextFactory>()
                   .PropertiesAutowired()
                   .InstancePerLifetimeScope();

            builder.Populate(services);
            return builder.Build().Resolve<IServiceProvider>();
        }

        /// <summary>
        /// Configures modules.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
        /// <param name="env"><see cref="IHostingEnvironment"/> instance.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> instance.</param>
        /// <remarks>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</remarks>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}