using Domain.Entities;

namespace Domain.IRepositories;

public interface ICartRepository
{
    Task<IEnumerable<OrderGame>> GetAllOrderGamesAsync();
    void UpdateOrderGame(OrderGame orderGame);
    Task<Order?> GetOpenOrderAsync();
    Task CreateOrderAsync(Order order);
    Task<Order> GetOrderByIdAsync(Guid orderId);
    Task<IEnumerable<Order>> GetPaidAndCancelledOrdersAsync();
    Task<IEnumerable<Order>> GetAllOrdersAsync();
}