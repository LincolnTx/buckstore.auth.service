using System;
using AutoMapper;
using buckstore.auth.service.application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.auth.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class AutoMapperSetup
	{
		public static void AddAutoMapper(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddAutoMapper(typeof(MappingProfile));
		}
	}
}