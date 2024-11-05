using Business.Dto.Product;
using Business.Services.Product;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RikaWebApp.Controllers.Product
{
    [Route("categories/{categoryName}/products")]
    public class ProductByCategoryController : Controller
    {
        private readonly IProductServiceCategory _productService;
        private const int PageSize = 6;

        public ProductByCategoryController(IProductServiceCategory productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(string categoryName, int page = 1)
        {
            try
            {
                var productsDto = await _productService.GetProductsByCategoryAsync(categoryName);

                int totalProducts = productsDto.Count();

                int totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);

                var pagedProducts = productsDto
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                var viewModel = pagedProducts.Select(ProductViewModel.FromDto).ToList();

                ViewData["Title"] = categoryName.First().ToString().ToUpper() + categoryName.Substring(1);
                ViewData["CategoryName"] = categoryName;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = page;
                return View("~/Views/Shared/Partials/Product/CategoryView.cshtml", viewModel);
                //return View("CategoryView", viewModel);
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
            return View("~/Views/Shared/Partials/Product/CategoryView.cshtml");
            //return View("CategoryView");
        }

    }
}



//using Business.Dto.Product;
//using Business.Services.Product;
//using Microsoft.AspNetCore.Mvc;
//using RikaWebApp.ViewModels;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace RikaWebApp.Controllers.Product
//{
//    [Route("categories/{categoryName}/products")]
//    public class ProductByCategoryController : Controller
//    {
//        private readonly IProductServiceCategory _productService;

//        public ProductByCategoryController(IProductServiceCategory productService)
//        {
//            _productService = productService;
//        }
//        [HttpGet("")]
//        public async Task<IActionResult> Index(string categoryName)
//        {
//            try
//            {
//                var productsDto = await _productService.GetProductsByCategoryAsync(categoryName);
//                var viewModel = productsDto.Select(ProductViewModel.FromDto).ToList();

//                ViewData["Title"] = categoryName.First().ToString().ToUpper() + categoryName.Substring(1);
//                ViewData["CategoryName"] = categoryName;

//                return View("CategoryView", viewModel);
//            }
//            catch (HttpRequestException)
//            {
//                ViewBag.ErrorMessage = "Unable to contact the server. Please try again later.";
//            }
//            catch (UnauthorizedAccessException)
//            {
//                ViewBag.ErrorMessage = "An error occurred. Please contact the web admin.";
//            }
//            catch (Exception)
//            {
//                ViewBag.ErrorMessage = "An unexpected error occurred.";
//            }

//            return View("CategoryView"); 
//        }
//    }
//}



