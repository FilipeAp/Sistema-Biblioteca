using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Pseudonimo
{
    public int PseudonimoId { get; set; }

    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Display(Name = "Pseudonimo do Autor")]
    public string NomePseudonimo { get; set; }


    public int AutorId { get; set; }
    public virtual Autor? Autor { get; set; }
}
