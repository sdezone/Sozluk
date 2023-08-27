using System;
using AutoMapper;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Models.Queries;

namespace Sozluk.Api.Application.Mapping
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<User, LoginUserViewModel>()
				.ReverseMap();
		}
		
	}
}

