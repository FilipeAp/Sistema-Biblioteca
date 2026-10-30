using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Livro
{
    public int LivroId { get; set; }

    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Required(ErrorMessage = "Informe um Título")]
    [Display(Name = "Nome do Título")]
    public string Titulo { get; set; }


    [Required(ErrorMessage = "O ISBN do livro é obrigatório.")]
    [StringLength(17, MinimumLength = 10, ErrorMessage = "O ISBN deve ter entre 10 e 17 caracteres.")]
    [RegularExpression(
    @"^(?:\d{10}|\d{13}|97[89](?:-\d+)*-\d+|(?:\d+-)+[\dXx])$",
    ErrorMessage = "Informe um ISBN-10 ou ISBN-13 válido, com ou sem hífens.")]
    [Display(Name = "ISBN do Livro", Prompt = "978-85-359-0277-1")]
    public string ISBN { get; set; }

    [Required(ErrorMessage = "O ano de publicação é obrigatório.")]
    [Range(1450, 2026, ErrorMessage = "O ano de publicação deve estar entre 1450 e o ano atual.")]
    [Display(Name = "Ano de Publicação", Prompt = "Ex: 2023")]
    public int Ano_Publicado { get; set; }


    public int CategoriaId { get; set; }
    public int SubcategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }
    public virtual Subcategoria Subcategoria { get; set; }

    public ICollection<Editora> Editoras { get; set; } = new List<Editora>();
    public ICollection<Copia> Copias { get; set; } = new List<Copia>();
    public ICollection<Autor> Autores { get; set; } = new List<Autor>();
}
