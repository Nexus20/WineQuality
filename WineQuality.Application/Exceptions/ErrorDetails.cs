using System.Text.Json;

namespace WineQuality.Application.Exceptions;

/// <summary>
/// Error occurred while processing a request.
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Error details constructor that receives an occurred error message.
    /// </summary>
    /// <param name="error">Occurred error message.</param>
    public ErrorDetails(string error)
    {
        Error = error;
    }

    /// <summary>
    /// Occurred error message.
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// Creates and returns a string representation of the error in JSON format.
    /// </summary>
    /// <returns>String representation of the error.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}