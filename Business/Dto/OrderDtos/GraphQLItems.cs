﻿using Business.Dto.Product;
using Newtonsoft.Json;

namespace Business.Dto.OrderDtos
{
    public class GraphQLItems
    {
        [JsonProperty("getProducts")]
        public List<ProductDTO>? GetProducts { get; set; }

        [JsonProperty("getProductsByIds")]
        public List<ProductDTO>? GetProductsByIds { get; set; }
    }
}
