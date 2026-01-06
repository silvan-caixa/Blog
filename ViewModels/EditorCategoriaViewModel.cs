using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
    {
    public class EditorCategoriaViewModel
        {
        [Required(ErrorMessage = "Campo 'Nome' obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Campo 'Nome' obrigatório mínimo 3 máximo 100")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo 'Slug' obrigatório")]
        public string Slug { get; set; } = string.Empty;
        }
    }
