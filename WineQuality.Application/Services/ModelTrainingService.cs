using System.Net.Http.Headers;
using Newtonsoft.Json;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Requests.GrapeSorts;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Services;

public class ModelTrainingService : IModelTrainingService
{
    private readonly HttpClient _httpClient;
    private readonly IUnitOfWork _unitOfWork;

    public ModelTrainingService(HttpClient client, IUnitOfWork unitOfWork)
    {
        _httpClient = client ?? throw new ArgumentNullException(nameof(client));
        _unitOfWork = unitOfWork;
    }

    public async Task<TrainModelResult> TrainPhaseModelAsync(GrapeSortPhaseDataset dataset, CancellationToken cancellationToken = default)
    {
        var request = new MlServiceTrainModelRequest()
        {
            DatasetUri = dataset.DatasetFileReference.Uri
        };

        var requestJson = JsonConvert.SerializeObject(request);
        var content = new StringContent(requestJson);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        var response = await _httpClient.PostAsync("/train", content, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new ModelTrainingErrorException(response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));

        var trainResultJson = await response.Content.ReadAsStringAsync(cancellationToken);

        var trainResult = JsonConvert.DeserializeObject<TrainModelResult>(trainResultJson);

        return trainResult;
    }
}