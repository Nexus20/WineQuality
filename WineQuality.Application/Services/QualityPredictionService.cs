using AutoMapper;
using Microsoft.Extensions.Localization;
using WineQuality.Application.Constants;
using WineQuality.Application.Constants.Localization;
using WineQuality.Application.Exceptions;
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

    public QualityPredictionService(IMachineLearningService machineLearningService, IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer)
    {
        _machineLearningService = machineLearningService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<PredictionDetailsResult> PredictQualityAsync(PredictQualityRequest request, CancellationToken cancellationToken = default)
    {
        var model = await _unitOfWork.GrapeSortPhaseForecastModels.GetByIdAsync(request.ForecastModelId, cancellationToken);
        
        if(model == null)
            throw new NotFoundException(nameof(GrapeSortPhaseForecastModel), nameof(request.ForecastModelId));

        var predictionResult = await _machineLearningService.PredictQualityAsync(model, request.ParametersValues, cancellationToken);
        var predictionDetails = CreatePredictionDetails(predictionResult, request.ParametersValues);
        return predictionDetails;
    }

    private PredictionDetailsResult CreatePredictionDetails(PredictionResult predictionResult,
        Dictionary<string, double> requestParametersValues)
    {
        var detailsResult = new PredictionDetailsResult();
        detailsResult.Quality = _stringLocalizer[QualityLocalizationConstants.QualityLocalizations[predictionResult.QualityPrediction]];

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
            var parts = key.Split(' ');
            string parameter;
            string condition;
            if (parts.Length == 4)
            {
                parameter = parts[1];
                condition = $"{parts[0]} {parts[2]} {parts[3]}";
            }
            else if (parts.Length == 5)
            {
                parameter = $"{parts[1]} {parts[2]}";
                condition = $"{parts[0]} {parts[3]} {parts[4]}";
            }
            else
            {
                continue;
            }

            string[] rangeParts = condition.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var lowerComparisonText = rangeParts[0].Contains('<') ? (rangeParts[0].Contains('=') ? "greater than or equal to" : "greater than") : "";
            var upperComparisonText = rangeParts[2].Contains('>') ? (rangeParts[2].Contains('=') ? "lower than or equal to" : "lower than") : "";

            var valueText = $"{lowerComparisonText} {rangeParts[1]} and {upperComparisonText} {rangeParts[3]}";
            var reason = $"{parameter} value is {valueText}";
            var value = inputParameters[parameter];

            explanationItems.Add(new QualityExplanationItem { Reason = reason, Value = value, Severity = severity });
        }

        return explanationItems;
    }

    public async Task<GrapeSortPhaseForecastModelResult> TrainModelByDatasetIdAsync(TrainPhaseModelRequest request, CancellationToken cancellationToken = default)
    {
        var dataset = await _unitOfWork.GrapeSortPhaseDatasets.GetByIdAsync(request.DatasetId, cancellationToken);
        
        if(dataset == null)
            throw new NotFoundException(nameof(GrapeSortPhaseDataset), nameof(request.DatasetId));
        
        var trainResult =
            await _machineLearningService.TrainPhaseModelAsync(dataset, cancellationToken: cancellationToken);

        var forecastModel = new GrapeSortPhaseForecastModel()
        {
            Accuracy = trainResult.Accuracy,
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