using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models;

public class Emprestimo
{
    public int EmprestimoId{ get; set; }

    [ScaffoldColumn(false)]
    [Required(ErrorMessage = "Informe a data de retirada do livro")]
    [Display(Name = "Data de retirada do livro")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime DataRetida{ get; set; }

    [Required(ErrorMessage = "Informe a data de previsão de devolução do livro")]
    [Display(Name = "Previsão de devolução")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly PrevisaoDevolucao { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Data de entrega")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime? DataEntregue { get; set; }


    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Multa")]
    public decimal Multa { get; set; }

    public int UsuarioId { get; set; }

    public int FuncionarioId { get; set; }

    public ICollection<Copia> Copias { get; set; } = new List<Copia>();
    public Usuario Usuario{ get; set; }
    public Funcionario Funcionario { get; set; }

}
