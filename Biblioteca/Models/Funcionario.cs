using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Funcionario
{
    public int FuncionarioId { get; set; }


    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "O CPF deve ter 14 caracteres (formatado).")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$|^\d{11}$",
        ErrorMessage = "Informe um CPF válido no formato 000.000.000-00 ou apenas os 11 números.")]
    [Display(Name = "CPF", Prompt = "000.000.000-00")]
    public string Cpf { get; set; }


    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Required(ErrorMessage = "Informe um nome")]
    [Display(Name = "Nome do Colaborador")]
    public string Nome { get; set; }


    [Required(ErrorMessage = "Informe o seu telefone")]
    [StringLength(25)]
    [DataType(DataType.PhoneNumber)]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Informe o email.")]
    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "O email não possui um formato correto")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Informe o logradouro")]
    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Display(Name = "Logradouro")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "Informe o número da casa")]
    [StringLength(5, ErrorMessage = "Tamanho máximo atingido (5 caracteres)")]
    [Display(Name = "Número da casa")]
    public string Numero { get; set; }

    [Required(ErrorMessage = "Informe o Bairro")]
    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Display(Name = "Bairro")]
    public string Bairro { get; set; }
    [Required(ErrorMessage = "Informe a Cidade")]
    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Display(Name = "Cidade")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "Informe o CEP")]
    [Display(Name = "CEP")]
    [StringLength(10, MinimumLength = 8)]
    public string Cep { get; set; }

    [StringLength(50, ErrorMessage = "Tamanho máximo atingido (50 caracteres)")]
    [Display(Name = "Complemento")]
    public string Complemento { get; set; }

    public ICollection<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
}
