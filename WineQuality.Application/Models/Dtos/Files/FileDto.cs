using WineQuality.Application.Extensions;

namespace WineQuality.Application.Models.Dtos.Files;

public class FileDto
{
    public Stream Content { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    
    public string GetPathWithFileName()
    {
        var uniqueRandomFileName = Path.GetRandomFileName();
        var shortClientSideFileNameWithoutExtension = Path.GetFileNameWithoutExtension(Name).TruncateLongString(20);
        var fileExtension = Path.GetExtension(Name);
        var basePath = "datasets/";

        return basePath + uniqueRandomFileName + "_" + shortClientSideFileNameWithoutExtension + fileExtension;
    }
}