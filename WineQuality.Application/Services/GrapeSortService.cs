using System.Linq.Expressions;
using AutoMapper;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.FileStorage;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Dtos.Files;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class GrapeSortService : IGrapeSortService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;
    private readonly IModelTrainingService _modelTrainingService;

    public GrapeSortService(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService, IModelTrainingService modelTrainingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
        _modelTrainingService = modelTrainingService;
    }

    public async Task<GrapeSortResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.GrapeSorts.GetByIdAsync(id, cancellationToken);

        if (source == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(id));

        var result = _mapper.Map<GrapeSort, GrapeSortResult>(source);

        return result;
    }

    public async Task<List<GrapeSortResult>> GetAsync(GetGrapeSortsRequest request, CancellationToken cancellationToken = default)
    {
        var predicate = CreateFilterPredicate(request);
        
        var source = await _unitOfWork.GrapeSorts.GetAsync(predicate, cancellationToken);

        if (source == null || !source.Any())
            return new List<GrapeSortResult>();

        var result = _mapper.Map<List<GrapeSort>, List<GrapeSortResult>>(source);

        return result;
    }

    public async Task<GrapeSortResult> CreateAsync(CreateGrapeSortRequest request, CancellationToken cancellationToken = default)
    {
        var duplicateExists = await _unitOfWork.GrapeSorts.ExistsAsync(x => x.Name == request.Name, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(GrapeSort)} with such name {request.Name} already exists");
        
        var grapeSortToAdd = _mapper.Map<CreateGrapeSortRequest, GrapeSort>(request);

        await _unitOfWork.GrapeSorts.AddAsync(grapeSortToAdd);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<GrapeSort, GrapeSortResult>(grapeSortToAdd);
        return result;
    }

    public async Task<GrapeSortResult> UpdateAsync(UpdateGrapeSortRequest request, CancellationToken cancellationToken = default)
    {
        var grapeSortToUpdate = await _unitOfWork.GrapeSorts.GetByIdAsync(request.Id, cancellationToken);
        
        if (grapeSortToUpdate == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(request.Id));
        
        var duplicateExists = await _unitOfWork.GrapeSorts.ExistsAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (duplicateExists)
            throw new ValidationException($"{nameof(GrapeSort)} with such name {request.Name} already exists");

        _mapper.Map(request, grapeSortToUpdate);

        _unitOfWork.GrapeSorts.Update(grapeSortToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<GrapeSort, GrapeSortResult>(grapeSortToUpdate);
        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var grapeSortToDelete = await _unitOfWork.GrapeSorts.GetByIdAsync(id, cancellationToken);
        
        if (grapeSortToDelete == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(id));
        
        _unitOfWork.GrapeSorts.Delete(grapeSortToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<GrapeSortPhaseDatasetResult>> AddPhaseForecastModelDatasetsAsync(AddGrapeSortPhaseForecastModelDatasetsRequest request, List<FileDto> filesDtos,
        CancellationToken cancellationToken = default)
    {

        if (filesDtos.Any(x => x.Content.Length == 0))
        {
            throw new ValidationException("Files cannot be empty");
        }
        
        var validTypes = new[] { "text/csv" };
        
        if (filesDtos.Any(x => !validTypes.Contains(x.ContentType)))
        {
            throw new ValidationException("Invalid file types");
        }
        
        var fileNamesWithUrls = await _fileStorageService.UploadAsync(filesDtos);
        
        var grapeSortPhase = await _unitOfWork.GrapeSortPhases.GetByIdAsync(request.GrapeSortPhaseId, cancellationToken: cancellationToken);

        if (grapeSortPhase == null)
            throw new NotFoundException(nameof(GrapeSortPhase), nameof(request.GrapeSortPhaseId));

        var datasetsToAdd = fileNamesWithUrls.Urls.Select(x => new GrapeSortPhaseDataset()
        {
            Name = x.Name,
            DatasetFileReference = new FileReference()
            {
                Uri = x.Url
            },
            GrapeSortPhaseId = grapeSortPhase.Id,
        }).ToList();
        
        grapeSortPhase.Datasets = datasetsToAdd;

        _unitOfWork.GrapeSortPhases.Update(grapeSortPhase);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<List<GrapeSortPhaseDataset>, List<GrapeSortPhaseDatasetResult>>(datasetsToAdd);
        
        return result;
    }

    public async Task<TrainModelResult> TrainModelByDatasetIdAsync(TrainPhaseModelRequest request, CancellationToken cancellationToken = default)
    {
        var dataset = await _unitOfWork.GrapeSortPhaseDatasets.GetByIdAsync(request.DatasetId, cancellationToken);
        
        if(dataset == null)
            throw new NotFoundException(nameof(GrapeSortPhaseDataset), nameof(request.DatasetId));
        
        var trainResult =
            await _modelTrainingService.TrainPhaseModelAsync(dataset, cancellationToken: cancellationToken);

        var forecastModel = new GrapeSortPhaseForecastModel()
        {
            Accuracy = trainResult.Accuracy,
            ForecastingModelFileReference = new FileReference()
            {
                Uri = trainResult.ModelUri
            },
            GrapeSortPhaseId = dataset.GrapeSortPhaseId
        };

        await _unitOfWork.GrapeSortPhaseForecastModels.AddAsync(forecastModel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return trainResult;
    }
    
    public async Task AssignGrapeSortToPhaseAsync(AssignGrapeSortToPhaseRequest request, CancellationToken cancellationToken = default)
    {
        var grapeSortPhaseWithSameOrderExists = await _unitOfWork.GrapeSortPhases.ExistsAsync(x => x.GrapeSortId == request.GrapeSortId && x.PhaseId == request.PhaseId && x.Order == request.Order, cancellationToken: cancellationToken);

        if (grapeSortPhaseWithSameOrderExists)
            throw new ValidationException($"Grape sort phase with order {request.Order} already exists");
        
        var grapeSort = await _unitOfWork.GrapeSorts.GetByIdAsync(request.GrapeSortId, cancellationToken);
        
        if (grapeSort == null)
            throw new NotFoundException(nameof(GrapeSort), nameof(request.GrapeSortId));
        
        var processPhase = await _unitOfWork.ProcessPhases.GetByIdAsync(request.PhaseId, cancellationToken);
        
        if (processPhase == null)
            throw new NotFoundException(nameof(ProcessPhase), nameof(request.PhaseId));

        var entityToAdd = _mapper.Map<AssignGrapeSortToPhaseRequest, GrapeSortPhase>(request);
        
        await _unitOfWork.GrapeSortPhases.AddAsync(entityToAdd, cancellationToken: cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<GrapeSortPhaseResult>> GetPhasesByGrapeSortIdAsync(string grapeSortId, CancellationToken cancellationToken = default)
    {
        var grapeSortExists = await _unitOfWork.GrapeSorts.ExistsAsync(x => x.Id == grapeSortId, cancellationToken);

        if (!grapeSortExists)
            throw new NotFoundException(nameof(GrapeSort), nameof(grapeSortId));

        var grapeSortPhases =
            await _unitOfWork.GrapeSortPhases.GetAsync(x => x.GrapeSortId == grapeSortId, cancellationToken);

        var result = _mapper.Map<List<GrapeSortPhase>, List<GrapeSortPhaseResult>>(grapeSortPhases);

        return result;
    }

    private Expression<Func<GrapeSort, bool>>? CreateFilterPredicate(GetGrapeSortsRequest request)
    {
        Expression<Func<GrapeSort, bool>>? predicate = null;

        // TODO: Add some code
        
        return predicate;
    }
}