using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CookieBasedAuth.Data;
using CookieBasedAuth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookieBasedAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _context;

        public AccountController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == input.Email && x.Password == input.Password);
                if (user != null)
                {
                    await Authenticate(input.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Некоректные логин и(или) пароль.");
            }

            return View(input);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == input.Email);
                if (user == null)
                {
                    await _context.Users.AddAsync(new User { Email = input.Email, Password = input.Password });
                    await _context.SaveChangesAsync();

                    await Authenticate(input.Email);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Некоректные логин и(или) пароль.");
            }

            return View(input);
        }


        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        private async Task Authenticate(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            string nameType = ClaimsIdentity.DefaultNameClaimType;
            string roleType = ClaimsIdentity.DefaultRoleClaimType;

            ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie", nameType, roleType);

            string scheme = CookieAuthenticationDefaults.AuthenticationScheme;

            await HttpContext.SignInAsync(scheme, new ClaimsPrincipal(identity));
        }
    }
}

