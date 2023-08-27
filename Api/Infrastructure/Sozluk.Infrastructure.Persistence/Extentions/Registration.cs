using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Infrastructure.Persistence.Context;
using Sozluk.Infrastructure.Persistence.Repositories;

namespace Sozluk.Infrastructure.Persistence.Extentions
{
	public static class Registration
	{
		public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddDbContext<SozlukContext>(conf =>
			{
				conf.UseSqlite(configuration.GetConnectionString("SozlukSqllite"));
			});

			//var seedData = new SeedData();
			//seedData.SeedAsync(configuration).GetAwaiter().GetResult();

			//inject repositories.
			services.AddScoped<IUserRepository, UserRepository>();
            return services;
		}
	}
}

