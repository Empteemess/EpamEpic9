using Domain.Entities;

namespace Domain.IRepositories;

public interface IPublisherRepository
{
    Task DeletePublisher(Guid publisherId);
    void UpdatePublisher(Publisher publisher);
    Task<Publisher> GetPublisherByCompanyNameAsync(string companyName);
    Task AddPublisherAsync(Publisher publisher);
    Task<Publisher> GetPublisherByIdAsync(Guid publisherId);
    Task<IEnumerable<Publisher>> GetAllPublishersAsync();
}