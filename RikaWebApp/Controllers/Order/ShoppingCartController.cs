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

        //public IActionResult Index()
        //{
        //    ShoppingCartViewModel viewModel = new ShoppingCartViewModel();
        //    PromoCodeFormModel form = new PromoCodeFormModel();
        //    var productList = _shoppingCartService.GetOneProductAsync();

        //    viewModel.Products = productList;
        //    viewModel.PromoCodeForm = form;

        //    return View(nameof(Index), viewModel);
        //}


        //public async Task<IActionResult> Index()
        //{
        //    var product = await _shoppingCartService.GetOneProductAsync();

        //    var viewModel = new ShoppingCartViewModel
        //    {
        //        Product = product
        //    };

        //    return View(viewModel);
        //}


        //FÖR ATT TESTA METODEN = 200 OK
        //public async Task<IActionResult> Index(int productId)
        //{
        //    var product = await _shoppingCartService.GetOneProductByIdAsync(productId);

        //    var viewModel = new ShoppingCartViewModel
        //    {
        //        Id = productId
        //    };

        //    return View(viewModel);
        //}


        public async Task<IActionResult> Index()
        {
            var currentUser = GetCookieInfoHelper.JwtTokenToBasicLoggedInUserModel(HttpContext);

            var shoppingcart = await _shoppingCartService.GetFullShoppingCart(currentUser.Email);


            List<string> ids = new List<string>();   

            foreach (var item in shoppingcart.CartItems!) 
                ids.Add(item.ProductId);

            var listOfProducts = await _shoppingCartService.GetAllCartItemsFromCart(ids);




            //var = await _shoppingcartservice.getuserbyemailasync(currentuser.email.tostring());


            var viewModel = new ShoppingCartViewModel
            {
                Email = currentUser.Email.ToString(),
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
