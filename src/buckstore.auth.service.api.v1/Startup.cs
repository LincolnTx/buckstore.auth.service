using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buckstore.auth.service.api.v1.Filters;
using buckstore.auth.service.application.CommandHandlers;
using buckstore.auth.service.infrastructure.CrossCutting.IoC.Configurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace buckstore.auth.service.api.v1
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options => options.AddPolicy("AllowCors", builder =>
			{
				builder.WithOrigins("*")
						.WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
						.AllowAnyHeader();
			}));

			services.AddSwaggerSetup();
			services.AddAutoMapper();
			services.AddDependencyInjectionSetup(Configuration);
			services.AddMediatR(typeof(CommandHandler));
			services.AddScoped<GlobalExceptionFilterAttribute>();
			services.AddDatabaseSetup();
			
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			
			app.UseRouting();

            app.UseSwaggerSetup();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}