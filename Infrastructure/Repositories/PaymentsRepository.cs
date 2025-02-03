using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentsRepository : IPaymentsRepository
{
    private readonly DbSet<PaymentMethod> _context;

    public PaymentsRepository(AppDbContext context)
    {
        _context = context.Set<PaymentMethod>();
    }

    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
    {
        return await _context.ToListAsync();
    }
}