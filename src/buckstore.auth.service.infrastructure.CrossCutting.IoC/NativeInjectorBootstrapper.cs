﻿using buckstore.auth.service.application.Services.AuthQueryServices;
using buckstore.auth.service.domain.Aggregates.UserAggregate;
using buckstore.auth.service.domain.Exceptions;
using buckstore.auth.service.domain.SeedWork;
using buckstore.auth.service.environment.Configuration;
using buckstore.auth.service.infrastructure.CrossCutting.identity.JwtIdentity;
using buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook;
using buckstore.auth.service.infrastructure.Data.Context;
using buckstore.auth.service.infrastructure.Data.Repositories.UserRepository;
using buckstore.auth.service.infrastructure.Data.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.auth.service.infrastructure.CrossCutting.IoC
{
	public class NativeInjectorBootstrapper
	{
		public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			RegisterData(services);
			RegisterMediatR(services);
			RegisterEnvironments(services, configuration);
			RegisterQueries(services);
			RegisterIdentityService(services);
		}

		public static void RegisterData(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IUserRepository, UserRepository>();
		}

		public static void RegisterQueries(IServiceCollection services)
		{
			services.AddScoped<IAuthQueryService, AuthQueryService>();
		}

		public static void RegisterMediatR(IServiceCollection services)
		{
			services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
		}

		public static void RegisterIdentityService(IServiceCollection services)
		{
			services.AddScoped<IIdentityService, IdentityService>();
			services.AddHttpClient<IFacebookIdendity, FacebookIdendity>();
		}

		public static void RegisterEnvironments(IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(configuration.GetSection("AppConfigurations").Get<AppConfigurations>());
			services.AddSingleton(configuration.GetSection("JwtSettings").Get<JwtSettings>());
			services.AddSingleton(configuration.GetSection("FacebookSettings").Get<FacebookSettings>());
		}
	}
}