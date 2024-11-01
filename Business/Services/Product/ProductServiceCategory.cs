using Business.Dto.Product;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


namespace Business.Services.Product
{
    public class ProductServiceCategory : IProductServiceCategory
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D";

        public ProductServiceCategory(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(string categoryName)
        {
            var query = new
            {
                query = @"query GetProductsByCategory($categoryName: String!) {
                    getProductsByCategory(categoryName: $categoryName) {
                        productId
                        name
                        description
                        images
                        category { name }
                        variants { size color stock price }
                        reviews { clientName rating comment }
                    }
                }",
                variables = new { categoryName }
            };

            var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            try
            {
                var result = JsonConvert.DeserializeObject<ProductResponse>(responseString);
                return result?.Data?.GetProductsByCategory ?? new List<ProductDTO>();
            }
            catch (JsonException)
            {
                return new List<ProductDTO>();
            }
        }
    }

    public class ProductResponse
    {
        public ProductData? Data { get; set; }
    }

    public class ProductData
    {
        public IEnumerable<ProductDTO> GetProductsByCategory { get; set; } = null!;
    }
}
