using Business.Dto.Product;
using Business.Services.Product.Backoffice;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.ViewModels;

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
        [Route("/backoffice/editproduct/{productId}")]
        public async Task<IActionResult> EditProduct(Guid productId)
        {
            var product = await _productBackofficeService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View("~/Views/ProductBackoffice/EditProduct.cshtml", product);
        }

        [HttpPost("/backoffice/editproduct/{productId}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, ProductDTO updatedProduct)
        {
            

            var success = await _productBackofficeService.UpdateProductAsync(productId, updatedProduct);

            if (!success)
            {
                ViewBag.ErrorMessage = "Failed to update product. Please try again later.";
                return View("~/Views/ProductBackoffice/EditProduct.cshtml", updatedProduct);

            }

            ViewBag.SuccessMessage = "Product has been successfully updated.";
            return View("~/Views/ProductBackoffice/EditProduct.cshtml", updatedProduct);

        }

    }
}
