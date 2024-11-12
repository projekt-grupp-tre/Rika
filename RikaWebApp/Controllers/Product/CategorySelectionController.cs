using Business.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using RikaWebApp.ViewModels;

namespace RikaWebApp.Controllers.Product
{
    public class CategorySelectionController : Controller
    {
        private readonly IProductServiceCategory _productServiceCategory;

        public CategorySelectionController(IProductServiceCategory productServiceCategory)
        {
            _productServiceCategory = productServiceCategory;
        }


      

        [Route("categories")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Categories";

        
            var allProducts = await _productServiceCategory.GetAllProductsAsync();


            var categories = allProducts
                .GroupBy(p => p.Category.Name)
                .Select(g => new CategoryProductCountViewModel
                {
                    CategoryName = g.Key, // 'Key' is the category name for this group
                    ProductCount = g.Count()
                })
                .ToList(); // Create a list of categories


            //var categories = new List<CategoryProductCountViewModel>
            //{
            //    new CategoryProductCountViewModel
            //    {
            //        CategoryName = "Clothes",
            //        ProductCount = allProducts.Count(p => p.Category.Name == "clothes"),
            //    },
            //    new CategoryProductCountViewModel
            //    {
            //        CategoryName = "Bags",
            //        ProductCount = allProducts.Count(p => p.Category.Name == "Bags")
            //    },
            //    new CategoryProductCountViewModel
            //    {
            //        CategoryName = "Shoes",
            //        ProductCount = allProducts.Count(p => p.Category.Name == "Shoes")
            //    },
            //    new CategoryProductCountViewModel
            //    {
            //        CategoryName = "Electronics",
            //        ProductCount = allProducts.Count(p => p.Category.Name == "Electronics")
            //    },
            //    new CategoryProductCountViewModel
            //    {
            //        CategoryName = "Jewelry",
            //        ProductCount = allProducts.Count(p => p.Category.Name == "Jewelry")
            //    }
            //};

            return View(categories);
        }
    }
}