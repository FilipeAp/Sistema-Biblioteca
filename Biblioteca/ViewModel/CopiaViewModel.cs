using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModel;

public class CopiaViewModel
{
    [Required(ErrorMessage = "Selecione um Livro")]
    public int? LivroId { get; set; }

    [Required(ErrorMessage = "Selecione um estado de conservação")]
    public int? Estado { get; set; }

    public IEnumerable<SelectListItem> Livros { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> Estados { get; set; } = new List<SelectListItem>();
}
