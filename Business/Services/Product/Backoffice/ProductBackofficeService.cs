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
    public async Task<ProductDTO?> GetProductByIdAsync(Guid productId)
    {
        var query = new
        {
            query = BackofficeQueries.GetProductByIdQuery,
            variables = new { productId }
        };

        var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(GraphQlServerUrl, content);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Failed to fetch product details from GraphQL server");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<GraphQLResponse<ProductDetailResponse>>(jsonResponse);

        return result?.Data?.GetProductById;
    }

    public async Task<bool> UpdateProductAsync(Guid productId, ProductDTO updatedProduct)
    {
        var queryObject = new
        {
            query = @"mutation UpdateProduct($productId: UUID!, $input: UpdateProductInput!) {
                            updateProduct(productId: $productId, input: $input) {
                                productId
                                name
                                description
                                images
                                category { name }
                                variants { productVariantId size color stock price }
                                reviews { reviewId clientName rating comment createdAt }
                            }
                        }",
            variables = new
            {
                productId,
                input = new
                {
                    productId = productId,
                    name = updatedProduct.Name,
                    description = updatedProduct.Description,
                    images = updatedProduct.Images,
                    categoryName = updatedProduct.Category.Name,
                    variants = (updatedProduct.Variants ?? new List<VariantDTO>()).Select(v => new
                    {
                        productVariantId = v.ProductVariantId,
                        size = v.Size,
                        color = v.Color,
                        stock = v.Stock,
                        price = v.Price
                    }).ToList(),
                    reviews = (updatedProduct.Reviews ?? new List<ReviewDTO>()).Select(r => new
                    {
                        reviewId = r.ReviewId,
                        clientName = r.ClientName,
                        rating = r.Rating,
                        comment = r.Comment,
                        createdAt = r.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    }).ToList()
                }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(GraphQlServerUrl, content);

        return response.IsSuccessStatusCode;
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
public class ProductDetailResponse
{
    public ProductDTO? GetProductById { get; set; }
}
