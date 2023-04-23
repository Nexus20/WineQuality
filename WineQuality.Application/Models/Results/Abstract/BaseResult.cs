namespace WineQuality.Application.Models.Results.Abstract;

public abstract class BaseResult
{
    public string Id { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}