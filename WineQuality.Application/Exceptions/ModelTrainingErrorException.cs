using System.Net;

namespace WineQuality.Application.Exceptions;

[Serializable]
public class ModelTrainingErrorException : Exception
{
    public ModelTrainingErrorException()
    {
    }
    
    public ModelTrainingErrorException(HttpStatusCode responseStatusCode, string message) : base(
        $"Model training request failed with code {responseStatusCode}. Details: {message}")
    {
    }
}