using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Campo obrigatorio")]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Campo obrigatorio")]
    public string Password { get; set; } = string.Empty;
}