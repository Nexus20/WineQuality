using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.Users;

public class SetLanguageRequest
{
    [Required] public string NewLanguage { get; set; } = null!;
}