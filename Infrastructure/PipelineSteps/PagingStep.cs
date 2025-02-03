using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;

namespace Infrastructure.PipelineSteps;

public class PagingStep : IPipelineStep<IQueryable<Game>>
{
    private readonly PaginationEnum _gameQuantity;
    private readonly int _currentPage;
    
    public PagingStep(PaginationEnum gameQuantity, int currentPage)
    {
        _gameQuantity = gameQuantity;
        _currentPage = currentPage;
    }
    
    public IQueryable<Game> Process(IQueryable<Game> game)
    {
        return _gameQuantity switch
        {
            PaginationEnum.Ten => game.Skip(10 * (_currentPage - 1)).Take(10),
            PaginationEnum.Twenty => game.Skip(20 * (_currentPage - 1)).Take(20),
            PaginationEnum.Fifty => game.Skip(50 * (_currentPage - 1)).Take(50),
            PaginationEnum.Hundred => game.Skip(100 * (_currentPage - 1)).Take(100),
            PaginationEnum.All => game,
            _ => game
        };
    }
}