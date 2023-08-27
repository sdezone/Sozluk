using System;
using FluentValidation;
using Sozluk.Common.Models.RequestModels;

namespace Sozluk.Api.Application.Features.Commands.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
	{
		public LoginUserCommandValidator()
		{
			RuleFor(i => i.EmailAddress)
				.NotNull()
				.EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
				.WithMessage("{PropertyName} is not valid email address");

			RuleFor(i => i.Password)
				.NotNull()
				.MinimumLength(6)
				.WithMessage("{PropertyName} should be at least {MinLenght} charaters");
		}
	}
}

