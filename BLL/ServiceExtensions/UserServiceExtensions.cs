using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.ServiceExtensions
{
    public static class UserServiceExtensions
    {
        public static IQueryable<User> Search(this IQueryable<User> users, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return users;
            }
            var lowerCaseSearchTerm = searchTerm.Trim().ToLowerInvariant();

            return users.Where(d => d.UserName.ToLower().Contains(lowerCaseSearchTerm));
        }

    }
}
