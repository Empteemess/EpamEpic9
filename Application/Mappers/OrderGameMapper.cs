using Application.Dtos.OrderGame;
using Domain.Entities;

namespace Application.Mappers;

public static class OrderGameMapper
{
    public static OrderGame MapToOrderGame(this Game game,Guid orderId)
    {
        var orderGame = new OrderGame
        {
            OrderId = orderId,
            ProductId = game.Id,
            Price = game.Price,
            Quantity = 1,
            Discount = game.Discount,
        };
        return orderGame;
    }
    
    public static OrderGameRequestDto MapToOrderGame(this OrderGame game)
    {
        var orderGame = new OrderGameRequestDto()
        {
            ProductId = game.Id,
            Price = game.Price,
            Quantity = 1,
            Discount = game.Discount,
        };
        return orderGame;
    }
}