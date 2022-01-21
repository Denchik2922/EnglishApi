using Microsoft.AspNetCore.Authorization;
using Models.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishApi.Infrastructure.AuthorizationHandlers
{
    public class DictionaryGetHandler : AuthorizationHandler<DictionaryGetRequirement, EnglishDictionary>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       DictionaryGetRequirement requirement,
                                                       EnglishDictionary resource)
        {
            if (context.User.FindFirst(ClaimTypes.NameIdentifier).Value == resource.UserId ||
                resource.IsPrivate == false)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class DictionaryGetRequirement : IAuthorizationRequirement { }
}
