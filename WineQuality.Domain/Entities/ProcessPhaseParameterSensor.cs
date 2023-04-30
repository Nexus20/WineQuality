﻿using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class ProcessPhaseParameterSensor : BaseEntity
{                                                                                  
    public string PhaseParameterId { get; set; } = null!;
    public virtual ProcessPhaseParameter PhaseParameter { get; set; } = null!;
    
    public string? WineMaterialBatchGrapeSortPhaseParameterId { get; set; }
    public virtual WineMaterialBatchGrapeSortPhaseParameter? WineMaterialBatchGrapeSortPhaseParameter { get; set; }

    public string DeviceKey { get; set; }
    public string DeviceName { get; set; }
    public bool IsActive { get; set; }
}