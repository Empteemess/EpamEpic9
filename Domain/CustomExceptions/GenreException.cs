namespace Domain.CustomExceptions;

public class GenreException : Exception
{
    public int StatusCode { get; }
    
    public GenreException(string message) : base(message)
    {
    }

    public GenreException(string message,int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}