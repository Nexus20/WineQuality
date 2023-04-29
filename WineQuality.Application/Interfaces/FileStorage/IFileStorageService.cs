using WineQuality.Application.Models.Dtos.Files;

namespace WineQuality.Application.Interfaces.FileStorage;

public interface IFileStorageService
{
    Task<UrlsDto> UploadAsync(List<FileDto> files);
    Task<bool> DeleteAsync(UrlsDto urls);
}