using Models.Entities;
using System.Collections.Generic;

namespace BLL.Tests.Infrastructure.Helpers
{
    public static class UserHelper
    {
        public static User GetOne(string id)
        {
            return new User()
            {
                Id = id,
                Email = "user@gmail.com"

            };
        }

        public static IEnumerable<User> GetMany()
        {
            yield return GetOne("b54e2482-5cd6-40d1-be4f-0d14e7e614e7");
        }
    }
}
