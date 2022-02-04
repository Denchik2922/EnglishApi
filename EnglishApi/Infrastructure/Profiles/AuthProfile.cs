using AutoMapper;
using EnglishApi.Dto.AuthDtos;
using EnglishApi.Dto.UserDtos;
using Microsoft.AspNetCore.Authentication;
using Models.Entities;

namespace EnglishApi.Infrastructure.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthenticationScheme, ProviderDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserRegisterDto>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
