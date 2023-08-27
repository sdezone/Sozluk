using System;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Sozluk.Api.Application.Extentions
{
	public static class Registration
	{
		public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
		{
			var assm = Assembly.GetExecutingAssembly();
			services.AddMediatR(assm);
			services.AddAutoMapper(assm);
			services.AddValidatorsFromAssembly(assm);

			return services;
		}
	}
}

