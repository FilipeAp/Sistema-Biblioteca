using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;

namespace Biblioteca.Repositories;

public class EditoraRepository : IEditoraRepository
{
    private readonly AppDbContext _context;

    public EditoraRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Editora> Editoras => _context.Editora;

    public async Task AddAsync(Editora editora)
    {
        _context.Add(editora);
        await _context.SaveChangesAsync();
    }
    public async Task<Editora?> GetByIdAsync(int? id)
    {
        return await _context.Editora.FindAsync(id);
    }

    public async Task UpdateEditora(Editora editora)
    {
        _context.Editora.Update(editora);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaEditora(Editora editora)
    {
        _context.Editora.Remove(editora);
        await _context.SaveChangesAsync();
    }
}
