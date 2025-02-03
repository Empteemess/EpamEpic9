using System.Net;
using Application.Dtos.Game;
using Application.Dtos.Publisher;
using Application.IServices;
using Application.Mappers;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using Infrastructure;
using Infrastructure.PipelineSteps;

namespace Application.Services;

public class PublisherService : IPublisherService
{
    private readonly IUnitOfWork _unitOfWork;

    public PublisherService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetPublisherDto> GetPublisherByGameKeyAsync(string key)
    {
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(key);
        if (game is null) throw new GameException("Game not found",(int)HttpStatusCode.NotFound);
        if (game.Publisher is null) throw new PublisherException("publisher not found",(int)HttpStatusCode.NotFound);
        
        return game.Publisher.MapToPublisherDto();
    }
    
    public async Task DeletePublisher(Guid publisherId)
    {
        await _unitOfWork.PublisherRepository.DeletePublisher(publisherId);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<GetGameDto>> GetGameByCompanyName(string companyName)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetPublisherByCompanyNameAsync(companyName);

        if (publisher is null) throw new PublisherException($"Publisher {companyName} not found",(int)HttpStatusCode.NotFound);
        if (publisher.Games is null) throw new GameException($"Game not found",(int)HttpStatusCode.NotFound);
        
        var games = publisher.Games.Select(x => x.MapToGetGameDto());
        
        return games;
    } 

    public async Task UpdatePublisher(UpdatePublisherDto updatePublisherDto)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetPublisherByIdAsync(updatePublisherDto.Id);
        if (publisher is null) throw new PublisherException("Publisher not found",(int)HttpStatusCode.NotFound);
        
        var updatedPublisher = publisher.MapToPublisher(updatePublisherDto);
        
        _unitOfWork.PublisherRepository.UpdatePublisher(updatedPublisher);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task<IEnumerable<GetPublisherDto>> GetAllPublishers(PublisherFilterDto publisherFilterDto)
    {
        var publishers = await _unitOfWork.PublisherRepository.GetAllPublishersAsync();

        var pipeline = new Pipeline<IQueryable<Publisher>>();

        TimeFilterOptionsEnum ss;
        
        if (publisherFilterDto.TimeFilterOptionsEnum is not null)
        {
            pipeline.AddStep(new TimeFilterStep(publisherFilterDto.TimeFilterOptionsEnum.Value));
        }

        var result = pipeline.Execute(publishers.AsQueryable());
        
        var resultList = result.ToList();
        
        return resultList.Select(x => x.MapToPublisherDto());
    }

    public async Task CreatePublisherAsync(PublisherRequestDto publisherRequestDto)
    {
        var publisherMapped = publisherRequestDto.MapToPublisher();
        
        var publisher = await _unitOfWork.PublisherRepository.GetPublisherByCompanyNameAsync(publisherMapped.CompanyName);
        if (publisher is not null) throw new PublisherException("Publisher already exists",(int)HttpStatusCode.Conflict);
        
        publisherMapped.PublishDate = DateTime.Now;
        await _unitOfWork.PublisherRepository.AddPublisherAsync(publisherMapped);
        await _unitOfWork.SaveAsync();
    }

    public async Task<GetPublisherDto> GetPublisherByCompanyNameAsync(string companyName)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetPublisherByCompanyNameAsync(companyName);
        return publisher.MapToPublisherDto();
    }
}