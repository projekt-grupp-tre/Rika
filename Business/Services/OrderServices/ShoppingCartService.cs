using Business.Dto.OrderDtos;
using Business.Interfaces.OrderInterfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Business.Services.OrderServices
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ValidatorResult Validate(CartItemDto cartItemDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCartDto> GetFullShoppingCart(string email)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://shoppingcartprovider-d9dcbqe8d7gnc6a3.westeurope-01.azurewebsites.net/cart?userEmail={email}");

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    ShoppingCartDto shoppingcart = JsonConvert.DeserializeObject<ShoppingCartDto>(json)!;

                    if (shoppingcart != null)
                        return shoppingcart;
                }
            }
            catch (Exception e) { Debug.Write("ERRORR ::: GetFullShoppingCart {0}", e.Message); }
            return null!;
        }

        public async Task<GraphQLResponse> GetAllCartItemsFromCart(List<string> ids)
        {
            var queryObject = new
            {
                query = @"
                         query GetProductsByIds($productIds: [UUID!]!) {
                           getProductsByIds(productIds: $productIds) {
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

                variables = new
                {
                    productIds = ids
                }
            };

            try
            {
                var jsonContent = JsonConvert.SerializeObject(queryObject);
                using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync("https://productprovidergraphql.azurewebsites.net/api/GraphQL?code=0GQhXGiLSYJRnfNBuRrB1_csNX6zQjBWwiUQgHPZb8pPAzFuI7EMSQ%3D%3D", content);

                if (result.IsSuccessStatusCode)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var graphQLResponse = JsonConvert.DeserializeObject<GraphQLResponse>(responseContent);

                    return graphQLResponse ?? new GraphQLResponse();
                }
            }
            catch (Exception e) { Debug.Write("ERRORR ::: GetAllCartItemsFromCart {0}", e.Message); }
            return null!;
        }
    }
}
