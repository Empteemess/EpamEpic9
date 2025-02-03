namespace Domain.CustomExceptions;

public class PlatformException : Exception
{
    public int StatusCode { get; }
    
    public PlatformException(string message) : base(message)
    {
    }

    public PlatformException(string message,int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}