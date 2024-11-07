using Business.Services.Product.Backoffice;
using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Product
{
    public class ProductBackofficeController(ProductBackofficeService productBackofficeService) : Controller
    {
        private readonly ProductBackofficeService _productBackofficeService = productBackofficeService;

        [Route("/backoffice")]
        public async Task<IActionResult> Index()
        {
            var products = await _productBackofficeService.GetBackofficeProductsAsync();
            return View(products);
        }

        [Route("/backoffice/addproduct")]
        public IActionResult AddProduct()
        {
            return View();
        }
    }
}
