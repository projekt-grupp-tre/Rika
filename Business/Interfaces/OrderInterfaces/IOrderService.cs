using Business.Dto.OrderDtos;

namespace Business.Interfaces.OrderInterfaces;

public interface IOrderService
{
    Task<ServiceResult> SaveOrderAsync(OrderDto order);
}
