namespace Domain.CustomExceptions;

public class GameException : Exception
{
    public int StatusCode { get; }
    
    public GameException(string message) : base(message)
    {
    }

    public GameException(string message,int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}