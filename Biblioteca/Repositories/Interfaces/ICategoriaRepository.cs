using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> Categorias { get;  }

    Task AddAsync(Categoria categoria);

    Task<Categoria?> GetByIdAsync(int? id);

    Task UpdateCategoria(Categoria categoria);
    Task DeletaCategoria(Categoria categoria);
}
