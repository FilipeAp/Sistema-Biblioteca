using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.ViewModels;

public class EmprestimoViewModel
{
    public int EmprestimoId { get; set; }

    [ScaffoldColumn(false)]
    [Required(ErrorMessage = "Informe a data de retirada do livro")]
    [Display(Name = "Data de retirada do livro")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime DataRetida { get; set; }

    [Required(ErrorMessage = "Informe a data de previsão de devolução do livro")]
    [Display(Name = "Previsão de devolução")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly PrevisaoDevolucao { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Data de entrega")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime DataEntregue { get; set; }


    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Multa")]
    public decimal Multa { get; set; }

    [Display(Name = "Nome do Leitor")]
    [Required(ErrorMessage = "Selecione um Leitor")]
    public int? UsuarioId { get; set; }

    public string NomeFuncionario { get; set; } = string.Empty;

    public string EmailFuncionario { get; set; } = string.Empty;


    // IDs selecionados no formulário
    [Display(Name = "ID da cópia")]
    public List<int> CopiaIds { get; set; } = new();

    // Opções exibidas nos campos de seleção
    public IEnumerable<SelectListItem> Copias { get; set; }
        = new List<SelectListItem>();

    public IEnumerable<SelectListItem> Usuarios { get; set; }
        = new List<SelectListItem>();


}
