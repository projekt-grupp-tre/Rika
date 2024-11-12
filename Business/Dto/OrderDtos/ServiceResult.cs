namespace Business.Dto.OrderDtos;

public class ServiceResult
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Result { get; set; }
}
