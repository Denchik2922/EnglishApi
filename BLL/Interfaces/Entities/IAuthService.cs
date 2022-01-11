﻿using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IAuthService
    {
        Task Register(User user, string password);
        Task<bool> ChangePassword(string UserId, string OldPassword, string NewPassword);
        Task<string> Authenticate(string username, string password);
    }
}