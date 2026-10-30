using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories.Interfaces;

public interface IEmprestimoRepository
{
    IEnumerable<Emprestimo> Emprestimos {  get; }
    IEnumerable<Copia> Copias { get; }
    IEnumerable<Funcionario> Funcionarios { get; }
    IEnumerable<Usuario> Usuarios { get; }
    

    Task RegistrarDevolucaoAsync(Emprestimo emprestimo);

    Task AddAsync(Emprestimo emprestimo, IEnumerable<int> copiasIds);

    Task<Emprestimo?> GetByIdAsync(int? id);

    Task UpdateEmprestimo(Emprestimo emprestimo, IEnumerable<int> copiasIds);
    Task DeletaEmprestimo(Emprestimo emprestimo);

}
