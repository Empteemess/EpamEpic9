using Application.Dtos.Publisher;
using Domain.Entities;

namespace Application.Mappers;

public static class PublisherMappers
{
    public static Publisher MapToPublisher(this PublisherRequestDto publisherRequestDto)
    {
        var publisher = new Publisher
        {
            CompanyName = publisherRequestDto.CompanyName,
            HomePage = publisherRequestDto.HomePage,
            Description = publisherRequestDto.Description,
        };
        
        return publisher;
    }

    public static Publisher MapToPublisher(this Publisher publisher,UpdatePublisherDto updatePublisherDto)
    {
        var publisherMapped = new Publisher()
        {
            CompanyName = updatePublisherDto.CompanyName,
            HomePage = updatePublisherDto.HomePage,
            Description = updatePublisherDto.Description,
        };
        
        return publisherMapped;
    }
    
    public static GetPublisherDto MapToPublisherDto(this Publisher publisher)
    {
        var publisherMapped = new GetPublisherDto()
        {
            Id = publisher.Id,
            CompanyName = publisher.CompanyName,
            HomePage = publisher.HomePage,
            Description = publisher.Description,
        };
        
        return publisherMapped;
    }
}