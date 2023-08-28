using App.Data.Abstract;
using App.Data.Context;
using App.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Mvc.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        readonly IRepository<CartEntity> _cartRepository;
        readonly IRepository<UserEntity> _userRepository;
        readonly IRepository<CartItemEntity> _cartItemRepository;

        public CartController(IRepository<CartEntity> cartRepository, IRepository<UserEntity> userRepository, IRepository<CartItemEntity> cartItemRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _cartItemRepository = cartItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var email = User.Identity.Name;
            var user = await _userRepository.Get
            ().FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var cart = await _cartRepository.Get()
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id && c.Name == "DefaultCart");

            if (cart == null)
            {
                cart = new CartEntity { UserId = user.Id, CreatedAt = DateTime.Now, Name = "DefaultCart" };
                _cartRepository.Create(cart);
            }

            return View(cart);
        }

        public async Task<IActionResult> List()
        {
            var email = User.Identity.Name;
            var user = await _userRepository.Get().Include(u => u.Carts).FirstOrDefaultAsync(u => u.Email == email);

            return View(user.Carts);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(string cartName)
        {
            if (!string.IsNullOrWhiteSpace(cartName))
            {
                var email = User.Identity.Name;
                var user = await _userRepository.Get().FirstOrDefaultAsync(u => u.Email == email);

                var cart = new CartEntity { UserId = user.Id, CreatedAt = DateTime.Now, Name = cartName };
                _cartRepository.Create(cart);
                

                return RedirectToAction("List", "Cart");
            }

            ModelState.AddModelError("cartName", "Cart Name is required");
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var email = User.Identity.Name;
            var user = await _userRepository.Get().FirstOrDefaultAsync(u => u.Email == email);

            var cart = await _cartRepository.Get().Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);

            if (cart == null)
                return NotFound();

            decimal total = 0m;

            foreach (var item in cart.CartItems)
                total += item.Quantity * item.Product.Price;

            ViewBag.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(int cartItemId, int quantityChange)
        {
            var cartItem = _cartItemRepository.GetById(cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantityChange;

                UpdateTotalPrice(cartItem, quantityChange);
            }

            return RedirectToAction("Detail", new { id = cartItem.CartId });
        }

        private void UpdateTotalPrice(CartItemEntity cartItem, int quantityChange)
        {
            decimal total = 0m;

            foreach (var item in cartItem.Product.CartItems)
            {
                if (item.Id == cartItem.Id)
                {
                    total += (item.Quantity + quantityChange) * item.Product.Price;
                }
            }
 
            ViewBag.TotalPrice = total;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            var email = User.Identity.Name;
            var user = await _userRepository.Get().FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            
            var cart = await _cartRepository.Get()
                .FirstOrDefaultAsync(c => c.UserId == user.Id && c.Id == cartId);

            if (cart != null)
            {
                _cartRepository.Remove(cart);       
            }

            return RedirectToAction("List", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            var cartItem = _cartItemRepository.GetById(cartItemId);

            if (cartItem != null)
            {
                _cartItemRepository.Delete(cartItemId);        
            }

            return RedirectToAction("Detail", new { id = cartItem.CartId });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity, int cartId)
        {
            var email = User.Identity.Name;
            var user = await _userRepository.Get().FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CartEntity cart = null;

            if (cartId != 0)
            {
                cart = await _cartRepository.Get()
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == user.Id && c.Id == cartId);
            }

            if (cart == null)
            {
                cart = await _cartRepository.Get()
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == user.Id && c.Name == "DefaultCart");

                if (cart == null)
                {
                    cart = new CartEntity { UserId = user.Id, CreatedAt = DateTime.Now, Name = "DefaultCart" };
                    _cartRepository.Create(cart);
                }
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItemEntity { CartId = cart.Id, ProductId = productId, Quantity = quantity };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _cartRepository.Update(cart.Id, cart);

            return RedirectToAction("Index", "Home");
        }
    }
}