namespace WineQuality.Application.Exceptions;

[Serializable]
public class IdentityException : ApplicationException
{
    public IdentityException()
    {
    }

    public IdentityException(string message) : base(message)
    {
    }

    public IdentityException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    // public IdentityException(IEnumerable<IdentityError> failures)
    // {
    //     Errors = failures
    //         .GroupBy(e => e.Code, e => e.Description)
    //         .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    // }
    
    public IDictionary<string, string[]> Errors { get; }
}