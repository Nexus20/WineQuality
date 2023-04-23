namespace WineQuality.Application.Exceptions;

[Serializable]
public class ValidationException : ApplicationException
{
    public ValidationException()
    {
    }

    public ValidationException(string message) : base(message)
    {
    }

    public ValidationException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}