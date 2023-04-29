using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using WineQuality.Application.Interfaces.FileStorage;
using WineQuality.Application.Models.Dtos.Files;

namespace WineQuality.Infrastructure.FileStorage;

public class BlobStorageService : IFileStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<UrlsDto> UploadAsync(List<FileDto> files)
    {
        if (files == null || files.Count == 0)
            return null;

        var containerClient = _blobServiceClient.GetBlobContainerClient("datasets");
        
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var urls = new List<FileNameWithUrlDto>();

        foreach (var file in files)
        {
            var blobClient = containerClient.GetBlobClient(file.GetPathWithFileName());
            await blobClient.UploadAsync(file.Content, new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            });

            urls.Add(new FileNameWithUrlDto()
            {
                Url = blobClient.Uri.ToString(),
                Name = file.Name
            });
        }

        return new UrlsDto(urls);
    }

    public async Task<bool> DeleteAsync(UrlsDto urls)
    {
        if (urls.Urls?.Any() == false)
            return true;
        
        var containerClient = _blobServiceClient.GetBlobContainerClient("datasets");
        
        foreach (var fileUrl in urls.Urls)
        {
            var fileName = Path.Combine(fileUrl.Url.Split('/').Skip(4).ToArray());

            var blobClient = containerClient.GetBlobClient(fileName);
            var response = await blobClient.DeleteAsync();

            if (response.IsError)
                return false;
        }

        return true;
    }
}