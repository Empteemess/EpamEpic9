namespace Domain.IRepositories;

public interface IPipelineStep<T> 
{
    T Process(T game);
}