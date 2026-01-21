using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Campo obrigatorio")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Campo obrigatorio")]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string Email { get; set; } = string.Empty;
}