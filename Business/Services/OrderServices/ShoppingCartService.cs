
using Business.Dto.OrderDtos;
using Business.Dto.Product;
using Business.Interfaces.OrderInterfaces;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Business.Services.OrderServices
{
    public class ShoppingCartService : IShoppingCartService
    {

        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        //public IEnumerable<ProductDto> GetProctsFromApi()
        //{
        //    List<ProductDto> products = new List<ProductDto>();

        //    for (int i = 0; i < 5; i++)
        //    {
        //        products.Add(new ProductDto
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            Name = "Dator",
        //            Description = "En slö dator",
        //            Category = "Datorer",
        //            Price = 10000
        //        });
        //    }

        //    return products;

        //}


        //public async Task<bool> AddProductToCartAsync(int userId, ProductDto productDto)
        //{
        //    try
        //    {
        //        var shoppingCartItem = new CartItemDto
        //        {
        //            UserId = userId,
        //            ProductId = productDto.Id,
        //            Quantity = 1
        //        };

        //        var json = JsonConvert.SerializeObject(shoppingCartItem);

        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = await _httpClient.PostAsync("", content);

        //        return response.IsSuccessStatusCode;
        //    }
        //    catch (Exception ex) { return false; }
        //}


        public async Task<ProductDto> GetOneProductAsync()
        {
            try
            {
                var query = @"{
                            ""query"": ""query GetProductById($productId: UUID!) { getProductById(productId: $productId) { productId name description images category { name } variants { size color stock price } reviews { clientName rating comment } } }"",
                            ""variables"": {
                                ""productId"": ""40ab5ee3cb974c50fff408dcf829356a""
                            }
                        }";

                using var content = new StringContent(query, Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync("https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D", content);

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(json);

                    
                    var productData = jsonObj?.data?.getProductById;

                    if (productData == null)
                    {
                        return null;
                    }

                    
                    return new ProductDto
                    {
                        Id = productData.productId?.ToString() ?? "Unknown",
                        Name = productData.name ?? "Unnamed Product",
                        Description = productData.description ?? "No Description",
                        Quantity = productData.quantity != null ? Convert.ToInt32(productData.quantity) : 0, 
                        Category = productData.category?.name ?? "No Category",
                        Price = productData.variants?[0]?.price != null ? (double?)productData.variants[0].price : null, 
                        Images = productData.images?.ToObject<List<string>>() ?? new List<string>() 
                    };
                }

                return null; 
            }
            catch (Exception ex) { }
            return null;
        }

    }

}
