using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sozluk.Infrastructure.Persistence.Context;

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

            return services;
		}
	}
}

