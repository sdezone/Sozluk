using System;
using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Exceptions;
using Sozluk.Common.Models.RequestModels;

namespace Sozluk.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);
            if (existUser is not null)
                throw new DatabaseValidationEception("user already exists.");

            var dbUser = mapper.Map<Domain.Models.User>(request);
            var rows = await userRepository.AddAsync(dbUser);
        }
    }
}

