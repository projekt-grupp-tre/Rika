using Business.Interfaces.OrderInterfaces;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.Models.OrderModels;
using RikaWebApp.ViewModels;

namespace RikaWebApp.Controllers.Order
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();
            PromoCodeFormModel form = new PromoCodeFormModel();
            var productsList = _shoppingCartService.GetProctsFromApi();

            viewModel.Products = productsList;
            viewModel.PromoCodeForm = form;

            return View(nameof(Index), viewModel);
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
