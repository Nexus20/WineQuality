using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.Roles;

public class CreateRoleRequest
{
    [Required] public string Name { get; set; } = null!;
}