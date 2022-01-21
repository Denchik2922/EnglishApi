using AutoMapper;
using BLL.Interfaces.Entities;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
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
            if(userModel == null)
            {
                return BadRequest("UserRegister object is null");
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            var user = _mapper.Map<User>(userModel);
            await _authService.Register(user, userModel.Password);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto model)
        {
            if (model == null)
            {
                return BadRequest("UserLogin object is null");
            }
            var token = await _authService.Authenticate(model.Username, model.Password);
            return Ok(token);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(UserToken tokenDto)
        {
            if (tokenDto == null)
            {
                return BadRequest("UserToken object is null");
            }
            var token = await _authService.RefreshAuth(tokenDto);
            return Ok(token);
        }

        [Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            var result = await _authService.ChangePassword(model.UserId, model.OldPassword, model.NewPassword);
            if (!result)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
