using Biblioteca.Models;
using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Repositories.Interfaces;

public interface IFuncionarioRepository
{
    IEnumerable<Funcionario> Funcionarios { get; }
    IEnumerable<IdentityUser> Users { get; }

    Task AddAsync(Funcionario funcionario);

    Task<Funcionario?> GetByIdAsync(int? id);

    Task UpdateFuncionario(Funcionario funcionario);
    Task DeletaFuncionario(Funcionario funcionario);
}
