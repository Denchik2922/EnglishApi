using AutoMapper;
using EnglishApi.Dto.UserDtos;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetailsDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
