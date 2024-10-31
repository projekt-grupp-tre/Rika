using Business.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RikaWebApp.Controllers.Product
{
    public class ProductByCategoryController : Controller
    {
        private readonly IProductServiceEdvin _productService;

        public ProductByCategoryController(IProductServiceEdvin productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string categoryName)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryName ?? "skor"); // Standardkategorin kan vara "skor"
            return View(products);
        }
    }
}
