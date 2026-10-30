using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Categoria
{

    public int CategoriaId { get; set; }

    [StringLength(100, ErrorMessage = "O tamanho máximo atingido (100 caracteres)")]
    [Required(ErrorMessage = "Informe um nome")]
    [Display(Name = "Nome da Categoria")]
    public string NomeCategoria { get; set; }
    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
