namespace WineQuality.Application.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException()
    {
        
    }

    public NotFoundException(string entityTypeName, object key) : base($"Entity {entityTypeName} with key {key} not found")
    {
        
    }
    
    public NotFoundException(string message) : base(message)
    {
        
    }
    
    public NotFoundException(string message, Exception? innerException) : base(message, innerException)
    {
        
    }
}