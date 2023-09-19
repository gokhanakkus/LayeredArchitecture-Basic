using App.Data.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using App.Mvc.Models;
using App.Data.Context;
using App.Data.Abstract;

namespace App.Mvc.Controllers
{
    public class AuthController : Controller
    {     
        readonly IRepository<CartEntity> _cartRepository;
        readonly IRepository<UserEntity> _userRepository;

        public AuthController(IRepository<CartEntity> cartRepository, IRepository<UserEntity> userRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.Get().FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    var darkModeCookie = Request.Cookies["DarkMode"];
                    var isDarkModeEnabled = darkModeCookie != null && bool.Parse(darkModeCookie);
                    // isDarkModeEnabled değerine göre stilleri ayarla

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (await _userRepository.Get().AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "A user with this email already exists.");
                    return View(model);
                }

                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                    return View(model);
                }

                var newUser = new UserEntity
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password
                };

                _userRepository.Create(newUser);



                var defaultCart = new CartEntity
                {
                    UserId = newUser.Id,
                    CreatedAt = DateTime.Now,
                    Name = "DefaultCart"
                };
                _cartRepository.Create(defaultCart);

                return RedirectToAction("Login");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete("DarkMode");

            return RedirectToAction("Index", "Home");
        }
    }
}