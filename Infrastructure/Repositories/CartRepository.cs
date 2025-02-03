using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DbSet<Order> _order;
    private readonly DbSet<OrderGame> _orderGame;

    public CartRepository(AppDbContext context)
    {
        _order = context.Set<Order>();
        _orderGame = context.Set<OrderGame>();
    }

    public void UpdateOrderGame(OrderGame orderGame)
    {
        _orderGame.Update(orderGame);
    }
    
    public async Task CreateOrderAsync(Order order)
    {
        await _order.AddAsync(order);
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _order.FirstOrDefaultAsync(x => x.Id == orderId);
        return order;
    }
    public async Task<Order?> GetOpenOrderAsync()
    {
        var orders = await _order
            .Include(x => x.OrderGames)
            .FirstOrDefaultAsync(x => x.Status == StatusEnum.Open);
        return orders;
    }
    public async Task<IEnumerable<Order>> GetPaidAndCancelledOrdersAsync()
    {
        var orders = await _order
            .Where(x => x.Status == StatusEnum.Paid && x.Status == StatusEnum.Cancelled)
            .ToListAsync();
        return orders;
    }
    
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _order.ToListAsync();
    }
    
    public async Task<IEnumerable<OrderGame>> GetAllOrderGamesAsync()
    {
        return await _orderGame.ToListAsync();
    }
}