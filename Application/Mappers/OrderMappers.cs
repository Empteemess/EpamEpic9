using Application.Dtos.Order;
using Application.Dtos.Payment;
using Domain.Entities;

namespace Application.Mappers;

public static class OrderMappers
{
    public static Order MapToOrder(this OrderRequestDto orderRequestDto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = orderRequestDto.CustomerId,
            Status = orderRequestDto.Status,
        };
        return order;
    }
    public static GetOrderDto MapToOrderDto(this Order order)
    {
        var orderDto = new GetOrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Date = order.Date,
        };
        return orderDto;
    }
    public static PaymentMethodsDto MaPaymentMethodsDto(this PaymentMethod paymentMethod)
    {
        var paymentMethodDto = new PaymentMethodsDto()
        {
            ImageUrl = paymentMethod.ImageUrl,
            Title = paymentMethod.Title,
            Description = paymentMethod.Description,
        };
        return paymentMethodDto;
    }
}