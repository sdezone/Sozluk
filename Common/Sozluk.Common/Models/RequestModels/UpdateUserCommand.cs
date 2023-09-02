using System;
using MediatR;

namespace Sozluk.Common.Models.RequestModels
{
	public class UpdateUserCommand:IRequest<Guid>
	{
        public Guid Id { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
    }
}

