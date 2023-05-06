using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.Services.Identity;
using WineQuality.Application.Models.Requests.Roles;
using WineQuality.Application.Models.Results.Roles;
using ValidationException = WineQuality.Application.Exceptions.ValidationException;

namespace WineQuality.Infrastructure.Identity.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<List<RoleResult>> GetAsync(CancellationToken cancellationToken = default)
    {
        var source = await _roleManager.Roles.ToListAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<List<AppRole>, List<RoleResult>>(source);
        return result;
    }

    public async Task<RoleResult> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var source = await _roleManager.FindByIdAsync(id);

        if(source == null)
            throw new NotFoundException(nameof(AppRole), nameof(id));
        
        var result = _mapper.Map<AppRole, RoleResult>(source);
        return result;
    }

    public async Task<RoleResult> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default)
    {
        var roleExists =
            await _roleManager.Roles.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

        if (roleExists)
            throw new ValidationException($"{nameof(AppRole)} with such email \"{request.Name}\" already exists");
        
        var roleToCreate = _mapper.Map<CreateRoleRequest, AppRole>(request);

        await _roleManager.CreateAsync(roleToCreate);

        var result = _mapper.Map<AppRole, RoleResult>(roleToCreate);

        return result;
    }
}