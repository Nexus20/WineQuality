﻿using WineQuality.Application.Models.Results.Abstract;

namespace WineQuality.Application.Models.Results.GrapeSorts;

public class GrapeSortPhaseForecastModelResult : BaseResult
{
    public decimal? Accuracy { get; set; }
    public string ModelUri { get; set; }
}