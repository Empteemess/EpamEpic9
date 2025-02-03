using Application.Dtos.Order;
using Application.Dtos.OrderGame;
using Application.Dtos.Payment;

namespace Domain.IRepositories;

public interface IOrderService
{
    Task<GetOrderDto> GetOrderByIdAsync(Guid orderId);
    Task<IEnumerable<OrderGameRequestDto>> AllOrderGamesAsync();
    Task<IEnumerable<PaymentMethodsDto>> AllPaymentMethodsAsync();
    Task DeleteGameAsync(string key);
    Task AddGameInCartAsync(string key);
    Task<Guid> CreateOrder();
}