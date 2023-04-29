﻿using WineQuality.Application.Models.Requests.ProcessPhaseTypes;
using WineQuality.Application.Models.Results.ProcessPhaseTypes;

namespace WineQuality.Application.Interfaces.Services;

public interface IProcessPhaseService {
    Task<ProcessPhaseResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<List<ProcessPhaseResult>> GetAsync(GetProcessPhasesRequest request, CancellationToken cancellationToken = default);
    Task<ProcessPhaseResult> CreateAsync(CreateProcessPhaseRequest request, CancellationToken cancellationToken = default);
    Task<ProcessPhaseResult> UpdateAsync(UpdateProcessPhaseRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task AddParameterAsync(AddParameterToPhaseRequest request, CancellationToken cancellationToken = default);
}