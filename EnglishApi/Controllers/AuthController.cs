using AutoMapper;
using BLL.Interfaces;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :  ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userModel)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            var user = _mapper.Map<User>(userModel);

            await _authService.Register(user, userModel.Password);

            return Ok();
        }
    }
}
