using System.IO.Pipelines;
using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure;

public class Pipeline<T>
{
    private List<IPipelineStep<T>> _pipelineStepsList = [];

    public Pipeline<T> AddStep(IPipelineStep<T> pipeline)
    {
        _pipelineStepsList.Add(pipeline);
        return this;
    }

    public T Execute(T input)
    {
        var result = _pipelineStepsList
            .Aggregate(input, (current, pipelineStep) => pipelineStep.Process(current));
    
        return result;
    }
}