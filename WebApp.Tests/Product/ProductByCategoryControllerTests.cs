using Business.Dto.Product;
using Business.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RikaWebApp.Controllers.Product;
using RikaWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RikaWebApp.Tests.Controllers.Product
{
    public class ProductByCategoryControllerTests
    {
        private readonly Mock<IProductServiceCategory> _mockProductServiceCategory;
        private readonly ProductByCategoryController _controller;

        public ProductByCategoryControllerTests()
        {
            _mockProductServiceCategory = new Mock<IProductServiceCategory>();
            _controller = new ProductByCategoryController(_mockProductServiceCategory.Object);
        }

        [Fact]
        public async Task Index_Should_ReturnViewWithProducts_WhenProductsExist()
        {
            // Arrange
            var categoryName = "Electronics";
            var products = new List<ProductDTO>
            {
                new ProductDTO
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Laptop",
                    Description = "A high-performance laptop",
                    Images = new List<string> { "image1.jpg", "image2.jpg" },
                    Category = new CategoryDTO { Name = categoryName },
                    Variants = new List<VariantDTO>
                    {
                        new VariantDTO { Size = "15 inch", Color = "Black", Stock = 10, Price = 999.99m }
                    },
                    Reviews = new List<ReviewDTO>
                    {
                        new ReviewDTO { ClientName = "John Doe", Rating = 5, Comment = "Excellent product!" }
                    }
                }
            };

            _mockProductServiceCategory.Setup(service => service.GetProductsByCategoryAsync(categoryName))
                                       .ReturnsAsync(products);

            // Act
            var result = await _controller.Index(categoryName);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(viewResult.Model);
            Assert.Single(model);
            Assert.Equal("Laptop", model.First().Name);
        }

        [Fact]
        public async Task Index_Should_ReturnErrorView_WhenHttpRequestExceptionOccurs()
        {
            // Arrange
            var categoryName = "Electronics";
            _mockProductServiceCategory.Setup(service => service.GetProductsByCategoryAsync(categoryName))
                                       .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _controller.Index(categoryName);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Unable to contact the server. Please try again later.", viewResult.ViewData["ErrorMessage"]);
        }

        [Fact]
        public async Task AllProducts_Should_ReturnViewWithAllProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<ProductDTO>
            {
                new ProductDTO
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Laptop",
                    Description = "A high-performance laptop",
                    Images = new List<string> { "image1.jpg", "image2.jpg" },
                    Category = new CategoryDTO { Name = "Electronics" },
                    Variants = new List<VariantDTO>
                    {
                        new VariantDTO { Size = "15 inch", Color = "Black", Stock = 10, Price = 999.99m }
                    },
                    Reviews = new List<ReviewDTO>
                    {
                        new ReviewDTO { ClientName = "John Doe", Rating = 5, Comment = "Excellent product!" }
                    }
                }
            };

            _mockProductServiceCategory.Setup(service => service.GetAllProductsAsync())
                                       .ReturnsAsync(products);

            // Act
            var result = await _controller.AllProducts();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(viewResult.Model);
            Assert.Single(model);
            Assert.Equal("Laptop", model.First().Name);
        }

        [Fact]
        public async Task AllProducts_Should_ReturnErrorView_WhenHttpRequestExceptionOccurs()
        {
            // Arrange
            _mockProductServiceCategory.Setup(service => service.GetAllProductsAsync())
                                       .ThrowsAsync(new HttpRequestException());

            // Act
            var result = await _controller.AllProducts();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Unable to contact the server. Please try again later.", viewResult.ViewData["ErrorMessage"]);
        }
    }
}
