﻿
namespace Business.Dto.OrderDtos
{
    public class ProductDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; } 
        public string? Category { get; set; }
        public double? Price { get; set; }
        public List<string>? Images { get; set; }
    }
}
