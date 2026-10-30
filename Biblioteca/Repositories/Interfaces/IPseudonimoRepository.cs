using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces;

public interface IPseudonimoRepository
{
    IEnumerable<Pseudonimo> Pseudonimos { get; }

    IEnumerable<Autor> Autores { get; }

    Task AddAsync(Pseudonimo pseudonimo);

    Task<Pseudonimo?> GetByIdAsync(int? id);

    Task UpdatePseudonimo(Pseudonimo pseudonimo);
    Task DeletaPseudonimo(Pseudonimo pseudonimo);
    Task<Autor?> GetAutorByIdAsync(int? id);
}
