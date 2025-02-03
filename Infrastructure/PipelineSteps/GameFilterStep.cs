using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.PipelineSteps;

public class GameFilterStep : IPipelineStep<IQueryable<Game>>
{
    private readonly Func<Game, bool> _filterCriteria;
    
       public GameFilterStep(Func<Game, bool> filterCriteria)
    {
        _filterCriteria = filterCriteria;
    }
    
    public IQueryable<Game> Process(IQueryable<Game> game)
    {
        return game.Where(_filterCriteria).AsQueryable();
    }
}