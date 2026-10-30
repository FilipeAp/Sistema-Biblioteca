using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Copia
{
    [Display(Name = "ID da Cópia")]
    public int CopiaId { get; set; }
    public int LivroId { get; set; }

    [Required]
    [Display(Name = "Estado de Conservação")]
    public EstadoConservacao EstadoConservacao { get; set; } = EstadoConservacao.Bom;

    [Required]
    [Display(Name = "Disponível para empréstimo")]
    public bool Disponivel { get; set; } = true;
    
    [ValidateNever]
    public Livro Livro { get; set; } = null!;
    public ICollection<Emprestimo> Emprestimos { get; set; }
    = new List<Emprestimo>();
}
