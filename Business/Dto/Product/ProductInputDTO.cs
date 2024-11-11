using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dto.Product;

public class ProductInputDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public List<string> Images { get; set; }
    public IEnumerable<ProductVariantDTO> Variants { get; set; }
    public IEnumerable<ProductReviewDTO> Reviews { get; set; }
}

public class ProductVariantDTO
{
    public string Size { get; set; }
    public string Color { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}

public class ProductReviewDTO
{
    public string ClientName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}
