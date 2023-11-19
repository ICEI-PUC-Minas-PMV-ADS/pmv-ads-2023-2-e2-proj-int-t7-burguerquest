using System.ComponentModel.DataAnnotations;
namespace Puc.ViewModel;
public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres de comprimento.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar Senha")]
    [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Endereco { get; set; }

}
