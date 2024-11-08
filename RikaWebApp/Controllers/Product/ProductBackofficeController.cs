
using Business.Dto.Product;
using Business.Services.Product.Backoffice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            return View("AddProduct");
        }

        [HttpPost]
        [Route("/backoffice/addproduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductInputDTO productInput)
        {
            try
            {
                // Anropa tjänsten utan att tilldela resultat till en variabel
                await _productBackofficeService.AddBackofficeProductAsync(productInput);

                // Omdirigera till "Index"-sidan efter att produkten har lagts till
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Required fields are missing or invalid.");
            }
            catch (InvalidOperationException invalidOpEx)
            {
                return StatusCode(400, "Invalid operation: " + invalidOpEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding the product. Please try again.");
            }
        }


    }
}
