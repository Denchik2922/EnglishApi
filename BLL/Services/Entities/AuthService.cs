using BLL.Exceptions;
using BLL.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenService _tokenService;

        public AuthService(UserManager<User> userManager, IJwtTokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task Register(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception($"Code: {error.Code}, Description: { error.Description}");
                }
            }
        }

        public async Task<bool> ChangePassword(string UserId, string OldPassword, string NewPassword)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new ItemNotFoundException($"{typeof(User).Name} item with id {UserId} not found.");
            }
            IdentityResult result =
                await _userManager.ChangePasswordAsync(user, OldPassword, NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception($"Code: {error.Code}, Description: { error.Description}");
                }
            }
            return result.Succeeded;
        }

        public async Task<UserToken> RefreshAuth(UserToken userToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(userToken.Token);
            var username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);
            if (user == null || 
                user.RefreshToken != userToken.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new Exception("Invalid client request");
            }

            var token = await _tokenService.GetToken(user);
            user.RefreshToken = _tokenService.GenerateRefreshToken();
            await _userManager.UpdateAsync(user);
            return new UserToken { Token = token, RefreshToken = user.RefreshToken };
        }

        public async Task<UserToken> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new ItemNotFoundException($"{typeof(User).Name} item with name {username} not found.");
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var token = await _tokenService.GetToken(user);
                user.RefreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _userManager.UpdateAsync(user);
                return new UserToken { Token = token, RefreshToken = user.RefreshToken };
            }
            else
            {
                throw new CheckUserPasswordException($"Password does not match the user {typeof(User).Name} with name {username}");
            }
        }
    }
}
