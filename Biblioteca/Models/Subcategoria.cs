using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Subcategoria
{
    public int SubcategoriaId { get; set; }


    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Required(ErrorMessage = "Informe um nome para a subcategoria")]
    [Display(Name = "Nome da subcategoria")]
    public string NomeSubcategoria { get; set; }


    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
