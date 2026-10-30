using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces;

public interface IUsuarioRepository
{
    IEnumerable<Usuario> Usuarios { get;  }

    Task AddAsync(Usuario usuario);

    Task<Usuario?> GetByIdAsync(int? id);

    Task UpdateUsuario(Usuario usuario);
    Task DeletaUsuario(Usuario usuario);
}
