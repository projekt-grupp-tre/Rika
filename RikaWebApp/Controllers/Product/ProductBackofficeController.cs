using Business.Dto.Product;
using Business.Services.Product.Backoffice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RikaWebApp.ViewModels;

namespace RikaWebApp.Controllers.Product
{
    public class ProductBackofficeController : Controller
    {
        private readonly ProductBackofficeService _productBackofficeService;
        private readonly ILogger<ProductBackofficeController> _logger;

        public ProductBackofficeController(ProductBackofficeService productBackofficeService, ILogger<ProductBackofficeController> logger)
        {
            _productBackofficeService = productBackofficeService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/backoffice")]
        public async Task<IActionResult> Index()
        {
            var products = await _productBackofficeService.GetBackofficeProductsAsync();
            return View("Index", products);
        }

        [HttpGet]
        [Route("/backoffice/addproduct")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Route("/backoffice/addproduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductInputDTO productInput)
        {
            try
            {
                // Call the service to add the product
                await _productBackofficeService.AddBackofficeProductAsync(productInput);

                
                TempData["SuccessMessage"] = "The product has been successfully added!";

               
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException)
            {
                TempData["ErrorMessage"] = "Required fields are missing or invalid.";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException invalidOpEx)
            {
                TempData["ErrorMessage"] = "Invalid operation: " + invalidOpEx.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while adding the product. Please try again.";
                return RedirectToAction("Index");
            }
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
