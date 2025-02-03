using Application.Dtos.Game;
using Application.Dtos.Publisher;

namespace Application.IServices;

public interface IPublisherService
{
    Task<GetPublisherDto> GetPublisherByGameKeyAsync(string key);
    Task DeletePublisher(Guid publisherId);
    Task<IEnumerable<GetGameDto>> GetGameByCompanyName(string companyName);
    Task<IEnumerable<GetPublisherDto>> GetAllPublishers(PublisherFilterDto publisherFilterDto);
    Task UpdatePublisher(UpdatePublisherDto updatePublisherDto);
    Task CreatePublisherAsync(PublisherRequestDto publisherRequestDto);
    Task<GetPublisherDto> GetPublisherByCompanyNameAsync(string companyName);
}