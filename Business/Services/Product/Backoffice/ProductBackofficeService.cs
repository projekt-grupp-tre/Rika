using Business.Dto.Product;
using Newtonsoft.Json;
using System.Text;

namespace Business.Services.Product.Backoffice;

public class ProductBackofficeService
{
    private readonly HttpClient _httpClient;
    private const string GraphQlServerUrl = "https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D";

    public ProductBackofficeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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

    public async Task AddBackofficeProductAsync(ProductInputDTO productInput)
    {
        try
        {
            var mutation = new
            {
                query = @"
                mutation AddClothingProduct($input: AddProductInput!) {
                    addProduct(input: $input) {
                        productId
                        name
                        description
                        category {
                            name
                        }
                        variants {
                            size
                            color
                            stock
                            price
                        }
                    }
                }",
                variables = new
                {
                    input = new
                    {
                        name = productInput.Name,
                        description = productInput.Description,
                        images = productInput.Images,
                        categoryName = productInput.CategoryName,
                        variants = productInput.Variants.Select(v => new
                        {
                            size = v.Size,
                            color = v.Color,
                            stock = v.Stock,
                            price = v.Price
                        }).ToList(),
                        reviews = new string[] { }
                    }
                }
            };

            var jsonContent = JsonConvert.SerializeObject(mutation);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, GraphQlServerUrl)
            {
                Content = content
            };
            requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Status code from GraphQL server: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response and check if the product was added
                var result = JsonConvert.DeserializeObject<GraphQLResponse<ProductAddResponse>>(responseContent);
                if (result?.Data?.AddProduct != null)
                {
                    Console.WriteLine("Product has been added successfully.");
                    return;
                }
                else
                {
                    throw new InvalidOperationException("Product creation failed: no product returned.");
                }
            }
            else
            {
                throw new HttpRequestException($"Failed to add product to GraphQL server. StatusCode: {response.StatusCode}, Error: {responseContent}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while adding the product", ex);
        }
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

public class ProductAddResponse
{
    public ProductBackofficeDTO? AddProduct { get; set; }
}
