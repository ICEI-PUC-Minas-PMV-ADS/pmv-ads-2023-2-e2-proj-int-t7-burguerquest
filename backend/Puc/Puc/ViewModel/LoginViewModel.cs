using System.ComponentModel.DataAnnotations;
namespace Puc.ViewModel;


public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    [Display(Name = "Lembrar de mim")]
    public bool RememberMe { get; set; }
}


