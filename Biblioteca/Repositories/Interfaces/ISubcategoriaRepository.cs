using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces;

public interface ISubcategoriaRepository
{
    IEnumerable<Subcategoria> Subcategorias { get;  }

    Task<Subcategoria?> GetByIdAsync(int? id);

    Task CriarSubcategoria(Subcategoria subcategoria);

    Task UpdateSubcategoria(Subcategoria categoria);
    Task DeletaSubcategoria(Subcategoria categoria);
}
