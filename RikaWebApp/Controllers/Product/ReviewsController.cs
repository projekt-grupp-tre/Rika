using Business.Services.Product;
using Microsoft.AspNetCore.Mvc;
using RikaWebApp.ViewModels;

namespace RikaWebApp.Controllers.Product
{
    [Route("categories/{categoryName}/products/{productId}/reviews")]
    public class ReviewsController : Controller
    {
        private readonly IProductReviewService _productReviewService;

        public ReviewsController(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
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
    }
}
