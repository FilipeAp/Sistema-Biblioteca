using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces
{
    public interface IAutorRepository
    {
        IEnumerable<Autor> Autores { get; }
        Task AddAsync(Autor autor);

        Task<Autor?> GetByIdAsync(int? id);

        Task UpdateAutor(Autor autor);
        Task DeletaAutor(Autor autor);
    }
}
