namespace WineQuality.Domain.Entities.Abstract;

public interface ITimeMarkedEntity
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}