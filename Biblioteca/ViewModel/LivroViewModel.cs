
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biblioteca.ViewModels;

public class LivroViewModel
{
    public int LivroId { get; set; }

    [Required(ErrorMessage = "Informe o título.")]
    [StringLength(100, ErrorMessage = "O título pode ter até 100 caracteres.")]
    [Display(Name = "Nome do Título")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ISBN é obrigatório.")]
    [StringLength(17, MinimumLength = 13)]
    [RegularExpression(
    @"^(?:\d{10}|\d{13}|97[89](?:-\d+)*-\d+|(?:\d+-)+[\dXx])$",
    ErrorMessage = "Informe um ISBN-10 ou ISBN-13 válido, com ou sem hífens."
)]    [Display(Name = "ISBN do Livro")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o ano de publicação.")]
    [Range(1450, 2026, ErrorMessage = "O ano deve estar entre 1450 e 2026.")]
    [Display(Name = "Ano de Publicação")]
    public int Ano_Publicado { get; set; }

    [Required(ErrorMessage = "Selecione uma categoria.")]
    public int? CategoriaId { get; set; }

    [Required(ErrorMessage = "Selecione uma subcategoria.")]
    public int? SubcategoriaId { get; set; }

    // IDs selecionados no formulário
    public List<int> AutoresIds { get; set; } = new();
    public List<int> EditorasIds { get; set; } = new();

    // Opções exibidas nos campos de seleção
    public IEnumerable<SelectListItem> Categorias { get; set; }
        = new List<SelectListItem>();

    public IEnumerable<SelectListItem> Subcategorias { get; set; }
        = new List<SelectListItem>();

    public IEnumerable<SelectListItem> Autores { get; set; }
        = new List<SelectListItem>();

    public IEnumerable<SelectListItem> Editoras { get; set; }
        = new List<SelectListItem>();
}