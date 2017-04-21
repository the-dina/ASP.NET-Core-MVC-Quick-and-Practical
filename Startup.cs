using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using EZCourse.Services;
using EZCourse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EZCourse
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();

			services.Configure<SmtpOptions>(Configuration);
			services.Configure<ContactOptions>(Configuration);
			services.Configure<AuthOptions>(Configuration);

			var connectionString = Configuration.GetConnectionString("EZCourseDatabase");
			services.AddDbContext<EZCourseContext>(options => options.UseSqlServer(connectionString));

			services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
			services.AddSingleton<Smtp, Smtp>();
			services.AddSingleton<Cryptography>();
			services.AddScoped<EZAuth>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "UserManagement",
					template: "Management/User/{action}/{id?}",
					defaults: new { controller = "UserManagement", action = "Index" });

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
