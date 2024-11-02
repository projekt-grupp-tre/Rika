using Business.Services.Product;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.ViewModels;

namespace RikaWebApp.Controllers;
[Route("products")]
public class ProductsController : Controller
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        // Hämta alla produkter, returnerar en lista av produker
        var productDto = await _productService.GetAllProductsAsync();
        //Console.WriteLine($"hittade {productDto.Count} produkter");


        //instans för att föra vidare data till vyn
        var viewModel = new ProductsPageViewModel
        {
            Title = "Alla Produkter",
            Products = productDto.Select(ProductViewModel.FromDto).ToList(),
        };

        return View("Partials/Product/Products", viewModel); 
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> ProductDetails(string categoryName, Guid id)
    {
        var productDto = await _productService.GetProductById(id);

        if (productDto == null)
        {
            return NotFound();
        }

        var productViewModel = ProductViewModel.FromDto(productDto);

        ViewData["CategoryName"] = categoryName;
        ViewData["ProductId"] = id;

        return View("Partials/Product/ProductDetails", productViewModel);
    }
}