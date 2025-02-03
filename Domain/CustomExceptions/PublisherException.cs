namespace Domain.CustomExceptions;

public class PublisherException : Exception
{
    public int StatusCode { get; }
    
    public PublisherException(string message) : base(message)
    {
    }

    public PublisherException(string message,int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}