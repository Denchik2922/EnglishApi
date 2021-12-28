using AutoMapper;
using EnglishApi.Dto;
using Models.Entities;
using System.Linq;

namespace EnglishApi.Infrastructure.Profiles
{
    public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(userDto => userDto.DictionaryTests, user => user.MapFrom(u => u.TestResults.Select(t => t.EnglishDictionary)));
			CreateMap<UserDto, User>();
		}
	}
}
