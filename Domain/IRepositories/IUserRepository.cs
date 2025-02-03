using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByIdAsync(string id);
    IQueryable<ApplicationUser> GetAllUserIQueryable();
}