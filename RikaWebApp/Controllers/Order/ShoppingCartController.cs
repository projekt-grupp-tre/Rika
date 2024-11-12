using Business.Dto.OrderDtos;
using Business.Interfaces.OrderInterfaces;
using Business.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.Helpers;
using RikaWebApp.Models.OrderModels;
using RikaWebApp.ViewModels;
using System.Diagnostics;


namespace RikaWebApp.Controllers.Order
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = GetCookieInfoHelper.JwtTokenToBasicLoggedInUserModel(HttpContext);
            var shoppingcart = await _shoppingCartService.GetFullShoppingCart(currentUser.Email);
            List<CartItemDto> cartItems = shoppingcart.CartItems!;
            List<string> ids = shoppingcart.CartItems!.Select(item => item.ProductId.ToString()).ToList();   

            var listOfProducts = await _shoppingCartService.GetAllCartItemsFromCart(ids);

            var viewModel = new ShoppingCartViewModel
            {
                ProductResponse = listOfProducts,
                CartItemDtos = cartItems,
                Email = currentUser.Email
            };

            return View(viewModel);
        }


        [Route("/shoppingcart/paymentmethod")]
        public async Task<IActionResult> PaymentMethod()
        {
            return View();
        }

        [Route("/shoppingcart/paymentdetails")]
        public async Task<IActionResult> PaymentDetails()
        {
            return View();
        }

        public IActionResult ValidatePromoCode(ShoppingCartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return View(nameof(Index), viewModel);
            }

            return View(nameof(Index), viewModel);
        }
    }
}
