using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces;

public interface IEditoraRepository
{
    IEnumerable<Editora> Editoras { get; }

    Task AddAsync(Editora editora);

    Task<Editora?> GetByIdAsync(int? id);

    Task UpdateEditora(Editora editora);
    Task DeletaEditora(Editora editora);
}
