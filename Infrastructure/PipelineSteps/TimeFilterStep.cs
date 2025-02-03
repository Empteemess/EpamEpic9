using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;

namespace Infrastructure.PipelineSteps;

public class TimeFilterStep : IPipelineStep<IQueryable<Publisher>>
{
    private readonly Func<Publisher, bool> _filterCriteria;

    public TimeFilterStep(TimeFilterOptionsEnum optionEnum)
    {
        _filterCriteria = publisher => ApplyTimeFilter(publisher, optionEnum);
    }

    private bool ApplyTimeFilter(Publisher publisher, TimeFilterOptionsEnum optionEnum)
    {
        var now = DateTime.Now;

        return optionEnum switch
        {
            TimeFilterOptionsEnum.LastWeek => publisher.PublishDate >= now.AddDays(-7),
            TimeFilterOptionsEnum.LastMonth => publisher.PublishDate >= now.AddMonths(-1),
            TimeFilterOptionsEnum.LastYear => publisher.PublishDate >= now.AddYears(-1),
            TimeFilterOptionsEnum.TwoYears => publisher.PublishDate >= now.AddYears(-2),
            TimeFilterOptionsEnum.ThreeYears => publisher.PublishDate >= now.AddYears(-3),
            _ => publisher.PublishDate >= now.AddYears(-3)
        };
    }

    public IQueryable<Publisher> Process(IQueryable<Publisher> game)
    {
        return game.Where(_filterCriteria).AsQueryable();
    }
}