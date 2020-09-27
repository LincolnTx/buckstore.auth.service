using System;
using buckstore.auth.service.infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.auth.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class DatabaseSetup
	{
		public static void AddDatabaseSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
		}
	}
}