using AutoMapper;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Interfaces.Services;
using WineQuality.Application.Models.Results.Cultures;

namespace WineQuality.Application.Services;

public class CultureService : ICultureService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CultureService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<CultureResult>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cultures = await _unitOfWork.Cultures.GetAllAsync(cancellationToken);

        return _mapper.Map<List<CultureResult>>(cultures);
    }
}