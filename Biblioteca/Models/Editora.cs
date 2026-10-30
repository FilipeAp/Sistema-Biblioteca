using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Editora
{
    public int EditoraId { get; set; }

    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Required(ErrorMessage = "Informe um nome")]
    [Display(Name = "Nome da Editora")]
    public string NomeEditora{ get; set; }

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

    [Required(ErrorMessage = "O CNPJ da editora é obrigatório.")]
    [StringLength(18, MinimumLength = 18, ErrorMessage = "O CNPJ deve ter 18 caracteres (formatado).")]
    [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$|^\d{14}$",
        ErrorMessage = "Informe um CNPJ válido no formato 00.000.000/0001-00")]
    [Display(Name = "CNPJ da Editora", Prompt = "00.000.000/0001-00")]
    public string CnpjEditora { get; set; }

    [Required(ErrorMessage = "O CNPJ da matriz é obrigatório.")]
    [StringLength(18, MinimumLength = 18, ErrorMessage = "O CNPJ deve ter 18 caracteres (formatado).")]
    [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$|^\d{14}$",
        ErrorMessage = "Informe um CNPJ válido no formato 00.000.000/0001-00")]
    [Display(Name = "CNPJ da Matriz", Prompt = "00.000.000/0001-00")]
    public string CnpjMatriz { get; set; }

    [Required(ErrorMessage = "Informe o logradouro")]
    [StringLength(100, ErrorMessage = "Tamanho máximo atingido (100 caracteres)")]
    [Display(Name = "Logradouro")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "Informe o número do estabelecimento")]
    [StringLength(5, ErrorMessage = "Tamanho máximo atingido")]
    [Display(Name = "Número do estabelecimento")]
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
    public string Complemento{ get; set; }


    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
