using LeaseIt.Cart;
using LeaseIt.Models;
using LeaseIt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LeaseIt.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IProductRepository productRepository, ShoppingCart shoppingCart, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = _orderRepository.GetOrdersByUserIdAndRoles(userId,userRole);
            return View(orders);

        }

        public IActionResult MyShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public RedirectToActionResult AddToShoppingCart(int id)
        {
            var item =  _productRepository.GetProductById(id);

            if(item != null)
            {
                _shoppingCart.AddToCart(item);
            }
            return RedirectToAction(nameof(MyShoppingCart));
        }

        public RedirectToActionResult RemoveFromShoppingCart(int id)
        {
            var item = _productRepository.GetProductById(id);

            if (item != null)
            {
                _shoppingCart.RemoveFromCart(item);
            }
            return RedirectToAction(nameof(MyShoppingCart));
        }

        public IActionResult CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.Identity.Name;

            _orderRepository.StoreOrder(items, userId, userEmailAddress);
            _shoppingCart.ClearShoppingCart();

            return View("OrderCompleted");
        }

    }
}
