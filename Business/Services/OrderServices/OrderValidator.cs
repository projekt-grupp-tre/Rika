using Business.Dto.OrderDtos;
using Business.Interfaces.OrderInterfaces;

namespace Business.Services.OrderServices;

public class OrderValidator : IOrderValidator
{
    public ValidatorResult Validate(OrderDto order)
    {
        throw new NotImplementedException();
    }

    public ValidatorResult ValidatePaymentMethod(string paymentMethod)
    {
        throw new NotImplementedException();
    }
}
