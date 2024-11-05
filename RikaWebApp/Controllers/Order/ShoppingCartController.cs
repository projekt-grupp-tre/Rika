using Business.Interfaces.OrderInterfaces;
using Business.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
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

        //public IActionResult Index()
        //{
        //    ShoppingCartViewModel viewModel = new ShoppingCartViewModel();
        //    PromoCodeFormModel form = new PromoCodeFormModel();
        //    var productList = _shoppingCartService.GetOneProductAsync();

        //    viewModel.Products = productList;
        //    viewModel.PromoCodeForm = form;

        //    return View(nameof(Index), viewModel);
        //}


        public async Task<IActionResult> Index()
        {
            var product = await _shoppingCartService.GetOneProductAsync();

            var viewModel = new ShoppingCartViewModel
            {
                Product = product 
            };

            return View(viewModel);
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
