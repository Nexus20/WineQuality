using WineQuality.Application.Models.Requests.Roles;
using WineQuality.Application.Models.Results.Roles;

namespace WineQuality.Application.Interfaces.Services.Identity;

public interface IRoleService
{
    Task<List<RoleResult>> GetAsync(CancellationToken cancellationToken = default);
    Task<RoleResult> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<RoleResult> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default);
}