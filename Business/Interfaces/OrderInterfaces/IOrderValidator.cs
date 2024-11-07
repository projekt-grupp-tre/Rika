using Business.Dto.OrderDtos;

namespace Business.Interfaces.OrderInterfaces;

public interface IOrderValidator
{
    ValidatorResult Validate(OrderDto order);

    ValidatorResult ValidatePaymentMethod(string paymentMethod);
}
