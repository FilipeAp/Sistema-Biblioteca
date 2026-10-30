using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories;

public class AutorRepository : IAutorRepository
{
    private readonly AppDbContext _context;

    public AutorRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Autor> Autores => _context.Autor.Include(p => p.Pseudonimos).Include(l => l.Livros);

    public async Task AddAsync(Autor autor)
    {
        _context.Add(autor);
        await _context.SaveChangesAsync();
    }
    public async Task<Autor?> GetByIdAsync(int? id)
    {
        return await _context.Autor.Include(a => a.Pseudonimos)
        .Include(a => a.Livros)
        .FirstOrDefaultAsync(a => a.AutorId == id);
    }

    public async Task UpdateAutor(Autor autor)
    {
        _context.Autor.Update(autor);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaAutor(Autor autor)
    {
        _context.Autor.Remove(autor);
        await _context.SaveChangesAsync();
    }
}
