using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces;

public interface IFuncionarioRepository
{
    IEnumerable<Funcionario> Funcionarios { get; }

    Task AddAsync(Funcionario funcionario);

    Task<Funcionario?> GetByIdAsync(int? id);

    Task UpdateFuncionario(Funcionario funcionario);
    Task DeletaFuncionario(Funcionario funcionario);
}
