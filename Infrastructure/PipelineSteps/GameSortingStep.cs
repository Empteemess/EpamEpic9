using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;

namespace Infrastructure.PipelineSteps;

public class GameSortingStep : IPipelineStep<IQueryable<Game>>
{
    private readonly Func<IQueryable<Game>,IOrderedQueryable<Game>> _sortFilter;
    
    public GameSortingStep(SortingEnum sortingEnum)
    {
        _sortFilter = filter => ApplyFilter(filter, sortingEnum);
    }
    
    public IOrderedQueryable<Game> ApplyFilter(IQueryable<Game> entities, SortingEnum sortingEnum)
    {
        return sortingEnum switch
        {
            SortingEnum.Popular => entities.OrderByDescending(x => x.Comments.Count),
            SortingEnum.New => entities.OrderByDescending(x => x.Price),
            SortingEnum.Commented => entities.OrderByDescending(x => x.Comments.Count),
            SortingEnum.PriceAscending => entities.OrderBy(x => x.Price),
            SortingEnum.PriceDescending => entities.OrderByDescending(x => x.Price),
            _ => (IOrderedQueryable<Game>)entities
        };
    }
    
    public IQueryable<Game> Process(IQueryable<Game> game)
    {
        return _sortFilter(game);
    }
}