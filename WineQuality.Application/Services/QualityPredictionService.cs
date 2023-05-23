using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.Extensions.Localization;
using WineQuality.Application.Constants;
using WineQuality.Application.Constants.Localization;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.FileStorage;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class QualityPredictionService : IQualityPredictionService
{
    private readonly IMachineLearningService _machineLearningService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResource> _stringLocalizer;
    private readonly IFileStorageService _fileStorageService;

    public QualityPredictionService(IMachineLearningService machineLearningService, IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer, IFileStorageService fileStorageService)
    {
        _machineLearningService = machineLearningService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _stringLocalizer = stringLocalizer;
        _fileStorageService = fileStorageService;
    }

    public async Task<PredictionDetailsResult> PredictQualityAsync(PredictQualityRequest request, CancellationToken cancellationToken = default)
    {
        var model = await _unitOfWork.GrapeSortPhaseForecastModels.GetByIdAsync(request.ForecastModelId, cancellationToken);
        
        if(model == null)
            throw new NotFoundException(nameof(GrapeSortPhaseForecastModel), nameof(request.ForecastModelId));

        var wineMaterialBatchGrapeSortPhase = await _unitOfWork.WineMaterialBatchGrapeSortPhases.GetByIdAsync(request.WineMaterialBatchGrapeSortPhaseId, cancellationToken);
        
        if(wineMaterialBatchGrapeSortPhase == null)
            throw new NotFoundException(nameof(WineMaterialBatchGrapeSortPhase), nameof(request.WineMaterialBatchGrapeSortPhaseId));
        
        if (request.ParametersValues == null || request.ParametersValues.Count == 0)
        {
            if(wineMaterialBatchGrapeSortPhase.Parameters.Any(x => x.Values == null || x.Values.Count == 0))
                throw new ValidationException("Not enough parameters values to predict quality");
            
            request.ParametersValues = wineMaterialBatchGrapeSortPhase.Parameters.ToDictionary(k => k.PhaseParameter.Parameter.Name, v => v.Values.OrderByDescending(x => x.CreatedAt).First().Value);
        }

        var predictionResult = await _machineLearningService.PredictQualityAsync(model, request.ParametersValues, cancellationToken);

        var qualityPredictionToSave = new QualityPrediction()
        {
            Prediction = predictionResult.QualityPrediction == 1,
            PredictionExplanation = predictionResult.PredictionExplanation,
            ParametersValues = request.ParametersValues,
            FileReference = new FileReference()
            {
                Uri = predictionResult.ExplanationUri
            },
            WineMaterialBatchGrapeSortPhaseId = wineMaterialBatchGrapeSortPhase.Id,
            WineMaterialBatchGrapeSortPhase = wineMaterialBatchGrapeSortPhase
        };
        
        await _unitOfWork.QualityPredictions.AddAsync(qualityPredictionToSave, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        var predictionDetails = CreatePredictionDetails(predictionResult, request.ParametersValues);
        return predictionDetails;
    }

    public async Task DeleteGrapeSortPhaseForecastModelAsync(string id, CancellationToken cancellationToken = default)
    {
        var model = await _unitOfWork.GrapeSortPhaseForecastModels.GetByIdAsync(id, cancellationToken);
        
        if(model == null)
            throw new NotFoundException(nameof(GrapeSortPhaseForecastModel), nameof(id));

        await _fileStorageService.DeleteAsync(model.ForecastingModelFileReference.Uri);
        
        _unitOfWork.FileReferences.Remove(model.ForecastingModelFileReference);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<PredictionDetailsResult>> GetGrapeSortPhasePredictionsHistoryAsync(string grapeSortPhaseId,
        CancellationToken cancellationToken)
    {
        var qualityPredictions =
            await _unitOfWork.QualityPredictions.GetAsync(x => x.WineMaterialBatchGrapeSortPhaseId == grapeSortPhaseId,
                cancellationToken);

        var result = qualityPredictions.Select(x =>
        {
            var predictionDetailsResult = new PredictionDetailsResult()
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Quality = _stringLocalizer[QualityLocalizationConstants.QualityLocalizations[x.Prediction ? 1 : 0]]
            };
            
            if(x.FileReference != null)
                predictionDetailsResult.ExplanationUri = x.FileReference.Uri;

            if (x is { PredictionExplanation: not null, ParametersValues: not null })
                predictionDetailsResult.ExplanationItems = ParseQualityExplanation(x.PredictionExplanation, x.ParametersValues);
            
            return predictionDetailsResult;
        }).OrderByDescending(x => x.CreatedAt).ToList();

        return result;
    }

    private PredictionDetailsResult CreatePredictionDetails(PredictionResult predictionResult,
        Dictionary<string, double> requestParametersValues)
    {
        var detailsResult = new PredictionDetailsResult();
        detailsResult.Quality = _stringLocalizer[QualityLocalizationConstants.QualityLocalizations[predictionResult.QualityPrediction]];
        detailsResult.ExplanationUri = predictionResult.ExplanationUri;
        if (predictionResult.QualityPrediction == QualityConstants.BadQuality)
        {
            detailsResult.ExplanationItems =
                ParseQualityExplanation(predictionResult.PredictionExplanation, requestParametersValues);
        }
        return detailsResult;
    }
    
    private List<QualityExplanationItem> ParseQualityExplanation(Dictionary<string, double> qualityExplanation, Dictionary<string, double> inputParameters)
    {
        var explanationItems = new List<QualityExplanationItem>();

        foreach (var (key, severity) in qualityExplanation)
        {
            
            if(severity < 0)
                continue;
            
            var match = Regex.Match(key, @"(\d+(\.\d+)?)\s*([<>]=?)\s*(\w+(\s\w+)?)\s*([<>]=?)\s*(\d+(\.\d+)?)");
            if (match.Success)
            {
                var value1 = match.Groups[1].Value;
                var value2 = match.Groups[7].Value;
                var parameter = match.Groups[4].Value;
                var condition1 = match.Groups[3].Value;
                var condition2 = match.Groups[6].Value;
                
                if(condition1 == "<")
                    condition1 = "greater than";
                if(condition1 == ">")
                    condition1 = "less than";
                if(condition1 == "<=")
                    condition1 = "greater or equal than";
                if(condition1 == ">=")
                    condition1 = "less or equal than";
                
                if(condition2 == "<")
                    condition2 = "less than";
                if(condition2 == ">")
                    condition2 = "greater than";
                if(condition2 == "<=")
                    condition2 = "less or equal than";
                if(condition2 == ">=")
                    condition2 = "greater or equal than";
                
                var reason = $"{parameter} is value is {condition1} {value1} and {condition2} {value2}";
                var value = inputParameters[parameter];

                explanationItems.Add(new QualityExplanationItem { Reason = reason, Value = value, Severity = severity });
            }
            else
            {
                match = Regex.Match(key, @"((\w+\s)?\w+)\s*([<>]=?)\s*(\d+(\.\d+)?)");
                
                if (match.Success)
                {
                    var parameter = match.Groups[1].Value;
                    var condition = key.Replace(parameter, "").Trim();
                    var reason = $"{parameter} value is {condition}";
                    var value = inputParameters[parameter];

                    explanationItems.Add(new QualityExplanationItem { Reason = reason, Value = value, Severity = severity });
                }
            }
        }
        return explanationItems;
    }

    public async Task<GrapeSortPhaseForecastModelResult> TrainModelByDatasetIdAsync(TrainPhaseModelRequest request, CancellationToken cancellationToken = default)
    {
        var dataset = await _unitOfWork.GrapeSortPhaseDatasets.GetByIdAsync(request.DatasetId, cancellationToken);
        
        if(dataset == null)
            throw new NotFoundException(nameof(GrapeSortPhaseDataset), nameof(request.DatasetId));
        
        var trainResult = await _machineLearningService.TrainPhaseModelAsync(dataset, cancellationToken: cancellationToken);
        
        var phaseName = dataset.GrapeSortPhase.Phase.Name.ToLower();

        var modelName = $"{phaseName}-";

        var phaseForecastModels = await _unitOfWork.GrapeSortPhaseForecastModels.GetAsync(x => x.GrapeSortPhaseId == dataset.GrapeSortPhaseId, cancellationToken: cancellationToken);

        if (!phaseForecastModels.Any())
        {
            modelName += "1";
        }
        else
        {
            var lastExistingModelNumber = phaseForecastModels.Select(x =>
                {
                    var nameParts = x.ModelName.Split('-');
                    return int.Parse(nameParts[^1]);
                })
                .Max();

            lastExistingModelNumber++;
            modelName += $"{lastExistingModelNumber}";
        }
        
        var forecastModel = new GrapeSortPhaseForecastModel()
        {
            Accuracy = trainResult.Accuracy,
            ModelName = modelName,
            ForecastingModelFileReference = new FileReference()
            {
                Uri = trainResult.ModelUri
            },
            GrapeSortPhaseId = dataset.GrapeSortPhaseId,
            Dataset = dataset,
            DatasetId = dataset.Id
        };
        await _unitOfWork.GrapeSortPhaseForecastModels.AddAsync(forecastModel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<GrapeSortPhaseForecastModel, GrapeSortPhaseForecastModelResult>(forecastModel);
        return result;
    }
    
    public async Task<List<GrapeSortPhaseForecastModelResult>> GetGrapeSortPhaseForecastModelsAsync(string grapeSortPhaseId,
        CancellationToken cancellationToken = default)
    {
        var source = await _unitOfWork.GrapeSortPhaseForecastModels.GetAsync(x => x.GrapeSortPhaseId == grapeSortPhaseId,
            cancellationToken: cancellationToken);

        var result = _mapper.Map<List<GrapeSortPhaseForecastModel>, List<GrapeSortPhaseForecastModelResult>>(source);

        return result;
    }
}