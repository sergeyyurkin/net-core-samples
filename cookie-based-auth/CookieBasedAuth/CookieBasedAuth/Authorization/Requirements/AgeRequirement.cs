using Microsoft.AspNetCore.Authorization;

namespace CookieBasedAuth.Authorization.Requirements
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        protected internal int Age { get; set; }

        public AgeRequirement(int age)
        {
            Age = age;
        }
    }
}
