using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IPaymentsRepository
{
    Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
}