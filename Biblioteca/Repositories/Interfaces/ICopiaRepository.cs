using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces;

public interface ICopiaRepository
{
    IEnumerable<Copia> Copias { get; }
    IEnumerable<Livro> Livros { get; }
    IEnumerable<Emprestimo> Emprestimos { get; }
    Task AddAsync(Copia copia);

    Task<Copia?> GetByIdAsync(int? id);
    Task<Livro?> GetByIdAsyncLivro(int? id);

    Task UpdateCopia(Copia copia);
    Task DeletaCopia(Copia copia);
}
