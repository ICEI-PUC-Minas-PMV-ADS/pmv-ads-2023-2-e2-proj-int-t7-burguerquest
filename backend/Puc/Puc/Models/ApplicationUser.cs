using Microsoft.AspNetCore.Identity;
namespace Puc.Models;

public class ApplicationUser : IdentityUser
{
    // Outras propriedades existentes, se houver

    public string Endereco { get; set; }
}