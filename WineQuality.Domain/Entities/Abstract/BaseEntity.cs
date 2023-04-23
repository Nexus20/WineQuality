﻿namespace WineQuality.Domain.Entities.Abstract;

public abstract class BaseEntity : ITimeMarkedEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}