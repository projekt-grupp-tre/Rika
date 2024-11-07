using Business.Dto.Product;
using Newtonsoft.Json;
using System.Text;

namespace Business.Services.Product.Backoffice;

public class ProductBackofficeService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;
    private const string GraphQlServerUrl = "https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D";

    public async Task<IEnumerable<ProductBackofficeDTO>> GetBackofficeProductsAsync()
    {
        var query = new
        {
            query = BackofficeQueries.GetProductsQuery
        };

        var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(GraphQlServerUrl, content);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Failed to fetch products from GraphQL server");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<GraphQLResponse<ProductResponse>>(jsonResponse);

        return result?.Data?.GetProducts?.Select(product => new ProductBackofficeDTO
        {
            ProductId = product.ProductId,
            Name = product.Name,
            CreatedAt = product.CreatedAt,
            Category = new CategoryDTO { Name = product.Category.Name }
        }) ?? new List<ProductBackofficeDTO>();
    }
}

public class GraphQLResponse<T>
{
    public T? Data { get; set; }
}

public class ProductResponse
{
    public IEnumerable<ProductBackofficeDTO>? GetProducts { get; set; }
}
