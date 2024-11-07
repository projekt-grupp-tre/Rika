using Business.Dto.Product;
using Business.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.ViewModels;
using System.Text;

namespace RikaWebApp.Controllers.Product;

[Route("categories/{categoryName}/products/{productId}/reviews")]
public class ReviewsController : Controller
{
    private readonly IProductReviewService _productReviewService;
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D";

    public ReviewsController(IProductReviewService productReviewService, IHttpClientFactory httpClientFactory)
    {
        _productReviewService = productReviewService;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string categoryName, Guid productId)
    {
        try
        {
            var reviews = await _productReviewService.GetReviewsByProductIdAsync(productId);
            if (reviews == null || !reviews.Any())
            {
                return View(Enumerable.Empty<ReviewViewModel>());
            }

            var reviewViewModels = reviews.Select(r => new ReviewViewModel
            {
                ClientName = r.ClientName,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();

            ViewData["Title"] = "Recensioner";
            ViewData["CategoryName"] = categoryName;
            ViewData["ProductId"] = productId;

            return View(reviewViewModels);
        }
        catch (HttpRequestException)
        {
            ViewBag.ErrorMessage = "Unable to contact the server. Please try again later.";
        }
        catch (Exception)
        {
            ViewBag.ErrorMessage = "An unexpected error occurred.";
        }

        return View(Enumerable.Empty<ReviewViewModel>());
    }

    [HttpGet("add-review")]
    public IActionResult AddReviewForm(string categoryName, Guid productId)
    {
        ViewData["CategoryName"] = categoryName;
        ViewData["ProductId"] = productId;
        return View("Partials/Product/AddReview");
    }

    [HttpPost("add-review")]
    public async Task<IActionResult> AddReview(string categoryName, Guid productId, ReviewViewModel reviewInput)
    {
        if (!ModelState.IsValid)
        {
            ViewData["CategoryName"] = categoryName;
            ViewData["ProductId"] = productId;
            ViewBag.ErrorMessage = "All fields are required.";
            return View("~/Views/Shared/Partials/Product/AddReview.cshtml", reviewInput);
        }

        var queryObject = new
        {
            query = @"mutation AddReviewToProduct($productId: UUID!, $input: ReviewInput!) {
                    addReviewToProduct(productId: $productId, input: $input) {
                        reviewId
                        clientName
                        rating
                        comment
                        createdAt
                    }
                }",
            variables = new
            {
                productId = productId,
                input = new
                {
                    clientName = reviewInput.ClientName,
                    rating = reviewInput.Rating,
                    comment = reviewInput.Comment
                }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_apiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            ViewData["CategoryName"] = categoryName;
            ViewData["ProductId"] = productId;
            ViewBag.ErrorMessage = "Failed to add review. Please try again later.";
            return View("~/Views/Shared/Partials/Product/AddReview.cshtml", reviewInput);
        }

        TempData["SuccessMessage"] = "Your review has been successfully added.";
        return RedirectToAction("Index", new { categoryName, productId });
    }
}
