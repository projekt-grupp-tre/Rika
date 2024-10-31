using RikaWebApp.ViewModels.Product;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Services.Product
{
    public class ProductServiceEdvin : IProductServiceEdvin
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D";

        public ProductServiceEdvin(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(string categoryName)
        {
            var query = new
            {
                query = @"query GetProductsByCategory($categoryName: String!) {
                        getProductsByCategory(categoryName: $categoryName) {
                            productId
                            name
                            description
                            images
                        }
                    }",


                variables = new { categoryName }
            };

            var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (!response.IsSuccessStatusCode)
                return new List<ProductModel>();

            var result = await response.Content.ReadFromJsonAsync<ProductResponse>();

            return result?.Data?.GetProductsByCategory ?? new List<ProductModel>();
        }
    }

    public class ProductResponse
    {
        public ProductData Data { get; set; }
    }

    public class ProductData
    {
        public IEnumerable<ProductModel> GetProductsByCategory { get; set; }
    }
}
