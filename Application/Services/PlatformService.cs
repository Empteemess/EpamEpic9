using System.Net;
using Application.Dtos.Platform;
using Application.IServices;
using Application.Mappers;
using Domain.CustomExceptions;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services;

public class PlatformService : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork;

    public PlatformService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddPlatformAsync(AddPlatformDto addPlatformDto)
    {
        var platformByType = await _unitOfWork.PlatformRepository.GetByTypeAsync(addPlatformDto.Type);
        if(platformByType is not null)throw new PlatformException($"Platform {addPlatformDto.Type} already exists",(int)HttpStatusCode.BadRequest);
        
        await _unitOfWork.PlatformRepository.AddAsync(addPlatformDto.ToAddPlatform());
        await _unitOfWork.SaveAsync();
    }

    public async Task<Platform> GetPlatformByIdAsync(Guid platformId)
    {
        var platorm = await _unitOfWork.PlatformRepository.GetByIdAsync(platformId);
        return platorm;
    }

    public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
    {
        var platforms = await _unitOfWork.PlatformRepository.GetAllAsync();
        return platforms;
    }

    public async Task<Platform> GetPlatformByGameKeyAsync(string gameKey)
    {
        var platform = await _unitOfWork.PlatformRepository.GetPlatformByGameKeyAsync(gameKey);
        return platform;
    }

    public async Task UpdatePlatformAsync(UpdatePlatformDto updatePlatformDto)
    {
        var platform = await _unitOfWork.PlatformRepository.GetByIdAsync(updatePlatformDto.Id);
        if(platform is null) throw new PlatformException("platform not found",(int)HttpStatusCode.NotFound);
        platform.UpdatePlatform(updatePlatformDto);
        
        _unitOfWork.PlatformRepository.Update(platform);        
        await _unitOfWork.SaveAsync();
    }

    public async Task DeletePlatformAsync(Guid platformId)
    {
        await _unitOfWork.PlatformRepository.DeleteAsync(platformId);
        await _unitOfWork.SaveAsync();
    }
}