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

public class DatasetService : IDatasetService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DatasetService(IFileStorageService fileStorageService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _fileStorageService = fileStorageService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GrapeSortPhaseDatasetResult>> GetGrapeSortPhaseDatasetsAsync(string grapeSortPhaseId,
        CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.GrapeSortPhaseDatasets.GetAsync(x => x.GrapeSortPhaseId == grapeSortPhaseId,
            cancellationToken: cancellationToken);

        var result = _mapper.Map<List<GrapeSortPhaseDataset>, List<GrapeSortPhaseDatasetResult>>(source);

        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var dataset = await _unitOfWork.GrapeSortPhaseDatasets.GetByIdAsync(id, cancellationToken);

        if (dataset == null)
            throw new NotFoundException(nameof(GrapeSortPhaseDataset), nameof(id));

        await _fileStorageService.DeleteAsync(dataset.DatasetFileReference.Uri);
        
        _unitOfWork.GrapeSortPhaseDatasets.Delete(dataset);
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
        
        var filesNames = filesDtos.Select(x => x.Name);

        var datasetExists = await _unitOfWork.GrapeSortPhaseDatasets.ExistsAsync(x =>
            filesNames.Contains(x.Name) && x.GrapeSortPhaseId == request.GrapeSortPhaseId, cancellationToken);

        if (datasetExists)
        {
            throw new ValidationException(
                $"For grape sort phase id {request.GrapeSortPhaseId} dataset with such name already exists");
        }

        var datasetsToAdd = fileNamesWithUrls.Urls.Select(x => new GrapeSortPhaseDataset()
        {
            Name = x.Name,
            DatasetFileReference = new FileReference()
            {
                Uri = x.Url
            },
            GrapeSortPhaseId = grapeSortPhase.Id,
        }).ToList();

        if (grapeSortPhase.Datasets != null && grapeSortPhase.Datasets.Any())
        {
            grapeSortPhase.Datasets.AddRange(datasetsToAdd);
        }
        else
        {
            grapeSortPhase.Datasets = datasetsToAdd;
        }

        _unitOfWork.GrapeSortPhases.Update(grapeSortPhase);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<List<GrapeSortPhaseDataset>, List<GrapeSortPhaseDatasetResult>>(datasetsToAdd);
        
        return result;
    }
    
    
}