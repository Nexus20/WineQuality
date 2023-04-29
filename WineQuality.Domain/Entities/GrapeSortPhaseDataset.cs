﻿using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class GrapeSortPhaseDataset : BaseEntity
{
    public string Name { get; set; } = null!;
    public string GrapeSortPhaseForecastModelId { get; set; } = null!;
    public virtual GrapeSortPhaseForecastModel GrapeSortPhaseForecastModel { get; set; } = null!;
    public string DatasetFileReferenceId { get; set; } = null!;
    public virtual FileReference DatasetFileReference { get; set; } = null!;
}