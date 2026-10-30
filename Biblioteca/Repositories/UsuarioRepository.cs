using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;

namespace Biblioteca.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;
    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Usuario> Usuarios => _context.Usuario;

    public async Task AddAsync(Usuario usuario)
    {
        _context.Add(usuario);
        await _context.SaveChangesAsync();
    }
    public async Task<Usuario?> GetByIdAsync(int? id)
    {
        return await _context.Usuario.FindAsync(id);
    }

    public async Task UpdateUsuario(Usuario usuario)
    {
        _context.Usuario.Update(usuario);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaUsuario(Usuario usuario)
    {
        _context.Usuario.Remove(usuario);
        await _context.SaveChangesAsync();
    }
}
