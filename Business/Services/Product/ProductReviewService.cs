using Business.Dto.Product;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Product;

public class ProductReviewService : IProductReviewService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D";

    public ProductReviewService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IEnumerable<ReviewDTO>> GetReviewsByProductIdAsync(Guid productId)
    {
        var queryObject = new
        {
            query = @"query ($productId: UUID!) {
                    getProductById(productId: $productId) {
                        reviews {
                            clientName
                            rating
                            comment
                            createdAt
                        }
                    }
                }",
            variables = new { productId = productId.ToString() }
        };

        var content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_apiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<GraphQLProductListResponse>(responseString);
        return result?.Data?.GetProductById?.Reviews ?? new List<ReviewDTO>();
    }
}

