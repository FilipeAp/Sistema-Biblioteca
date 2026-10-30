using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Autor
{

    public int AutorId { get; set; }

    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")] 
    [Required(ErrorMessage = "Informe um nome")] 
    [Display(Name = "Nome do Autor")]
    public string NomeAutor{ get; set; }

    public ICollection<Pseudonimo> Pseudonimos { get; set; } = new List<Pseudonimo>();
    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
