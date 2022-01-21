using Microsoft.AspNetCore.Authorization;
using Models.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishApi.Infrastructure.AuthorizationHandlers
{
    public class DictionaryEditHandler : AuthorizationHandler<DictionaryEditRequirement, EnglishDictionary>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       DictionaryEditRequirement requirement,
                                                       EnglishDictionary resource)
        {
            if (context.User.FindFirst(ClaimTypes.NameIdentifier).Value == resource.UserId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class DictionaryEditRequirement : IAuthorizationRequirement { }
}
