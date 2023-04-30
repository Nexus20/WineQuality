namespace WineQuality.Application.Exceptions;

[Serializable]
public class InvalidSensorStatusException : InvalidOperationException
{
    public InvalidSensorStatusException()
    {
        
    }

    public InvalidSensorStatusException(string message) : base(message)
    {
        
    }
}