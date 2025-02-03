namespace Domain.CustomExceptions;

public class AuthException : Exception
{
    public int StatusCode { get; }
    
    public AuthException(string message) : base(message)
    {
    }

    public AuthException(string message,int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}