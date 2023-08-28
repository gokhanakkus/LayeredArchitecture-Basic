using App.Data.Abstract;
using App.Data.Context;
using App.Data.Entities;
using App.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace App.Mvc.Controllers
{
    public class HomeController : Controller
    {
        readonly IRepository<ProductEntity> _productRepository;
        readonly IRepository<UserEntity> _userRepository;

        public HomeController(IRepository<ProductEntity> productRepository, IRepository<UserEntity> userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.Get().ToListAsync();

            var email = User.Identity.Name;
            var user = await _userRepository.Get().Include(u => u.Carts).FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                ViewBag.UserCarts = user.Carts.ToList();

                //Kullanıcı sepetleri taşıma
                ViewBag.UserCartOptions = new SelectList(user.Carts, "Id", "Name");
            }

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}