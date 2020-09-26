using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBasedAuth.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            Claim claim = User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
            return Content($"User: {User.Identity.Name}, Role: {claim.Value}");
        }

        [Authorize(Roles = "admin")]
        public IActionResult About()
        {
            return Content("Вход только для администратора");
        }
    }
}
