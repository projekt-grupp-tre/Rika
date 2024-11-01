using Business.Dto.Product;
using Business.Services.Product;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RikaWebApp.Controllers.Product
{
    public class ProductByCategoryController : Controller
    {
        private readonly IProductServiceCategory _productService;

        public ProductByCategoryController(IProductServiceCategory productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string categoryName = "clothes")
        {
            try
            {
                var productsDto = await _productService.GetProductsByCategoryAsync(categoryName);
                var viewModel = productsDto.Select(ProductViewModel.FromDto).ToList();

                ViewData["Title"] = categoryName.First().ToString().ToUpper() + categoryName.Substring(1);

                return View("CategoryView", viewModel);
            }
            catch (HttpRequestException)
            {
                ViewBag.ErrorMessage = "Unable to contact the server. Please try again later.";
            }
            catch (UnauthorizedAccessException)
            {
                ViewBag.ErrorMessage = "An error occurred. Please contact the web admin.";
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An unexpected error occurred.";
            }

            return View("CategoryView"); 
        }
    }
}



