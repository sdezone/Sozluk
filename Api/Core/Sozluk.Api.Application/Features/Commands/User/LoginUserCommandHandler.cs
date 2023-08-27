using System;
using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Exceptions;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Models.Queries;
using Sozluk.Common.Models.RequestModels;

namespace Sozluk.Api.Application.Features.Commands.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);
            if (dbUser == null)
                throw new DatabaseValidationEception("User not found.");

            var pass = PasswordEncryptor.Encryp(request.Password);
            if (dbUser.Password != pass)
                throw new DatabaseValidationEception("Password is wrong.");

            if (dbUser.EmailConfirmed)
                throw new DatabaseValidationEception("Emai not confirmed");

            var result = mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email,dbUser.EmailAddress),
                new Claim(ClaimTypes.Name,dbUser.UserName),
                new Claim(ClaimTypes.GivenName,dbUser.FirsName),
                new Claim(ClaimTypes.Surname,dbUser.LastName)

            };

            result.Token = JwtToken.GenerateToken(claims);
            return result;
        }
    }
}

