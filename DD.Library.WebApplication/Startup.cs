using AutoMapper;
using DD.Library.Model.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using DD.Library.Logger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace DD.Library.WebApplication
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var mapperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);
			services.AddControllers();
			services.AddSwaggerGen(options => {
				//Determine base path for the application.
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				//Set the comments path for the swagger json and ui.
				var xmlPath = Path.Combine(basePath, "LibraryApi.xml");
				options.IncludeXmlComments(xmlPath);
			});

			services.AddSingleton<ILog, LogNLog>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILog logger)
		{
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.ConfigureExceptionHandler(logger);
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
			    name: "default",
			    pattern: "{controller=Library}/{action}/{id?}");
			});
		}
	}
}
