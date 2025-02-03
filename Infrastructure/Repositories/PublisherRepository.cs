using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly DbSet<Publisher> _publisher;

    public PublisherRepository(AppDbContext context)
    {
        _publisher = context.Set<Publisher>();
    }
    
    public async Task DeletePublisher(Guid publisherId)
    {
        var publisher = await GetPublisherByIdAsync(publisherId);
        
        _publisher.Remove(publisher);
    }
    
    public void UpdatePublisher(Publisher publisher)
    {
        _publisher.Update(publisher);
    }
    
    public async Task AddPublisherAsync(Publisher publisher)
    {
        await _publisher.AddAsync(publisher);
    }
    
    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
    {
        return await _publisher.ToListAsync();
    }

    public async Task<Publisher> GetPublisherByCompanyNameAsync(string companyName)
    {
        var publisher = await _publisher
            .Include(g => g.Games)
            .FirstOrDefaultAsync(cn => cn.CompanyName == companyName);
        return publisher;
    }
    public async Task<Publisher> GetPublisherByIdAsync(Guid publisherId)
    {
        var publisher = await _publisher.FirstOrDefaultAsync(p => p.Id == publisherId);
        return publisher;
    }
    
}