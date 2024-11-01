using Business.Dto.Product;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Business.Services.Product;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    ///     Hämtar en lista av alla produkter från API:et
    /// </summary>
    /// <returns>En lista med produkter, om inget finns returneras inget</returns>
    public async Task<List<ProductDTO>> GetAllProductsAsync()
    {
        try
        {
            var queryObject = new
            {
                query = @"
                    query {
                      getProducts {
                        productId
                        name
                        description
                        images
                        category {
                          name
                        }
                        variants {
                          size
                          color
                          stock
                          price
                        }
                        reviews {
                          clientName
                          rating
                          comment
                        }
                      }
                    }"
            };

            var content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json");

            //post förfrågan till graphql API:et med det serializerade innehållet
            var response = await _httpClient.PostAsync("https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                //packa upp svaret 
                var result = JsonConvert.DeserializeObject<GraphQLProductListResponse>(responseString);
                return result?.Data?.GetProducts!;
            }
            return null!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett oväntat fel inträffade: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    ///     Hämtar en specifik produkt baserat på produkt id
    /// </summary>
    /// <param name="productId">Produktens unika id som ska hämtas</param>
    /// <returns>En produkt med information om den specifika produkten, annars inget</returns>
    public async Task<ProductDTO> GetProductById(Guid productId)
    {
        try
        {
            var queryObject = new
            {
                query = @"
                    query ($productId: UUID!) {
                      getProductById(productId: $productId) {
                        productId
                        name
                        description
                        images
                        category {
                          name
                        }
                        variants {
                          size
                          color
                          stock
                          price
                        }
                        reviews {
                          clientName
                          rating
                          comment
                        }
                      }
                    }",
                variables = new { productId = productId.ToString() }
            };

            var content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json");

            //post förfrågan till API:et med det serializerade innehållet
            var response = await _httpClient.PostAsync("https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GraphQLProductListResponse>(responseString);
                return result?.Data?.GetProductById!;
            }
            return null!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett oväntat fel inträffade: {ex.Message}");
            throw;
        }
    }
}

public class GraphQLProductListResponse
{
    //innehåller data som returneras från API:et
    public ProductListData? Data { get; set; }
}

public class ProductListData
{
    [JsonProperty("getProducts")]
    public List<ProductDTO>? GetProducts { get; set; }

    [JsonProperty("getProductById")]
    public ProductDTO? GetProductById { get; set; }
}
