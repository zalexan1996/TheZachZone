using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TZZ.Domain.Entities.TheZachZone;

public class User : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string DisplayName => FirstName is null ? UserName ?? Email! : $"{FirstName} {LastName ?? string.Empty}";
}
