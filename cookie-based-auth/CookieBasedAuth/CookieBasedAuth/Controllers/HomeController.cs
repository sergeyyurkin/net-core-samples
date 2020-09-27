using System.Security.Claims;
using System.Text;
using CookieBasedAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBasedAuth.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return GetUserInfo();
        }

        [Authorize(Roles = "admin")]
        public IActionResult OnlyForAdmin()
        {
            return GetUserInfo();
        }

        [Authorize(Policy = AppAuthPolicy.OnlyForLondon)]
        public IActionResult OnlyForLondon()
        {
            return GetUserInfo();
        }

        [Authorize(Roles = AppAuthPolicy.OnlyForMicrosoft)]
        public IActionResult OnlyForMicrosoft()
        {
            return GetUserInfo();
        }

        private ContentResult GetUserInfo()
        {
            var stringBuilder = new StringBuilder()
                .AppendLine("Path: " + HttpContext.Request.Path)
                .AppendLine("User: " + User.Identity.Name)
                .AppendLine("Claims:");

            foreach(var claim in User.Claims)
                stringBuilder.AppendLine($" - {claim.Type}: {claim.Value}");

            return Content(stringBuilder.ToString());
        }
    }
}
