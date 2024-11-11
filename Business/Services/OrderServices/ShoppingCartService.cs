
using Business.Dto.OrderDtos;
using Business.Dto.Product;
using Business.Interfaces.OrderInterfaces;
using Business.Services.Product;
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

        #region AddProductToCartItemAsync
        public async Task<bool> SendCartItemAsync(string email, string productId)
        {
            try
            {
                var shoppingCartItem = new CartItemDto
                {
                    Email = email,
                    ProductId = productId,
                    Quantity = 1 //dynamiskt
                };

                var json = JsonConvert.SerializeObject(shoppingCartItem);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { return false; }
        }
        #endregion

        #region GetOne (ta bort)
        // Ta bort denna? Använd produktteamets metod.
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

        public async Task<string?> GetOneProductByIdAsync(int productId)
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
                    dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(json)!;

                    var productData = jsonObj?.data?.getProductById;

                    if (productData == null)
                    {
                        return null;
                    }

                    return productData.productId?.ToString();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region GetOneUserByEmailAsync
        public async Task<string> GetUserByEmailAsync(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://rikaregistrationapi-ewdqdmb7ayhwhkaw.westeurope-01.azurewebsites.net/api/GetUser?email={email}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic user = JsonConvert.DeserializeObject<dynamic>(json)!;

                    if (user != null && user.email != null) 
                    {
                        return user.email.ToString();
                   
                    }else {  return null!; }
                }
                return null!;
            }
            catch (Exception)
            {
                return null!;
            }

        }
        #endregion

        #region AddProductToCartAsync
        public async Task<ResponseDto> AddProductToCartAsync(string email, string productId)
        {
            try
            {
                var user = await GetUserByEmailAsync(email);
                if (user == null)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Could not find user"
                    };

                    //skapa nytt sessionsId om användaren inte är inloggad
                }

                var product = await GetOneProductAsync();
                if (product == null || product?.Quantity <= 0) 
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Product out of stock"
                    };
                }

                var addToCart = await SendCartItemAsync(email, productId);
                if (addToCart)
                {
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Message = "Added to cart."
                    };
                }

                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Failed to add to cart"
                };
            }
            catch (Exception)
            {
                return null!;
            }
        }

        #endregion

        public ValidatorResult Validate(CartItemDto cartItemDto)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCartDto> GetFullShoppingCart(string email)
        {
            throw new NotImplementedException();
        }
    }
}
