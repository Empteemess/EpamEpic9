using System.Net;
using Application.Dtos.Order;
using Application.Dtos.OrderGame;
using Application.Dtos.Payment;
using Application.Mappers;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetOrderDto> GetOrderByIdAsync(Guid orderId)
    {
        var orderDto = await _unitOfWork.CartRepository.GetOrderByIdAsync(orderId);
        return orderDto.MapToOrderDto();
    }
    public async Task<IEnumerable<OrderGameRequestDto>> AllOrderGamesAsync()
    {
        var orderGameRequestDto = await _unitOfWork.CartRepository.GetAllOrderGamesAsync();
        return orderGameRequestDto.Select(x => x.MapToOrderGame());
    }
    public async Task<IEnumerable<PaymentMethodsDto>> AllPaymentMethodsAsync()
    {
        var paymentMethods = await _unitOfWork.PaymentsRepository.GetPaymentMethodsAsync();
        return paymentMethods.Select(x => x.MaPaymentMethodsDto());
    }
    
    public async Task DeleteGameAsync(string key)
    {
        var order = await _unitOfWork.CartRepository.GetOpenOrderAsync();
        if (order is null) throw new NullReferenceException("Order not found");

        var game = await _unitOfWork.GameRepository.GetByKeyAsync(key);
        if (game is null) throw new GameException("Game not found", (int)HttpStatusCode.NotFound);

        var orderGame = order.OrderGames.FirstOrDefault(x => x.ProductId == game.Id);

        order.OrderGames.Remove(orderGame);

        await _unitOfWork.SaveAsync();
    }

    public async Task AddGameInCartAsync(string key)
    {
        var order = await _unitOfWork.CartRepository.GetOpenOrderAsync();
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(key);
    
        if (order == null)
        {
            var newOrderId = await CreateOrder();

            var newOrder = await _unitOfWork.CartRepository.GetOrderByIdAsync(newOrderId);
           
            newOrder.OrderGames.Add(game.MapToOrderGame(newOrderId));
        }
        else
        {
            var existingOrderGame = order.OrderGames.FirstOrDefault(x => x.ProductId == game.Id);

            if (existingOrderGame == null)
            {
                order.OrderGames.Add(game.MapToOrderGame(order.Id));
            }
            else
            {
                existingOrderGame.Quantity += 1;
                _unitOfWork.CartRepository.UpdateOrderGame(existingOrderGame);
            }
        }

        await _unitOfWork.SaveAsync();
    }

    public async Task<Guid> CreateOrder()
    {
        var order = await _unitOfWork.CartRepository.GetOpenOrderAsync();

        var orderId = Guid.NewGuid();

        if (order is null)
        {
            var newOrder = new Order
            {
                Id = orderId,
                Date = DateTime.Now,
                CustomerId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Status = StatusEnum.Open
            };

            await _unitOfWork.CartRepository.CreateOrderAsync(newOrder);
        }

        await _unitOfWork.SaveAsync();

        return orderId;
    }
}