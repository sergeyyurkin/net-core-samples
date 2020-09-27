using System;
using System.Threading.Tasks;
using CookieBasedAuth.Authorization.Requirements;
using CookieBasedAuth.Models;
using Microsoft.AspNetCore.Authorization;

namespace CookieBasedAuth.Authorization.Handlers
{
    public class AgeHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(c => c.Type == AppClaimTypes.YearOfBirth);
            if (dateOfBirthClaim != null)
            {
                if (Int32.TryParse(dateOfBirthClaim.Value, out int dateOfBirthYear))
                {
                    if (DateTime.Now.Year - dateOfBirthYear >= requirement.Age)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
