namespace Business.Dto.OrderDtos;

public class OrderDto
{
    public Guid OrdertId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string DeliveryAddress { get; set; }
    public List<ProductDto> Products { get; set; }
    public string ShippingMethod { get; set; }
    public string PaymentMethod { get; set; }
    public decimal TotalPrice { get; set; }
    public string PromoCode { get; set; }
}
