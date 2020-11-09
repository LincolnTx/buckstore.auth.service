using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.auth.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class DependencyInjectionSetup
	{
		public static void AddDependencyInjectionSetup(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			NativeInjectorBootstrapper.RegisterServices(services, configuration);
		}
	}
}