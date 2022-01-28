using AutoMapper;
using EnglishApi.Dto;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
