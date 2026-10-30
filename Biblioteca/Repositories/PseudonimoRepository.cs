using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;

namespace Biblioteca.Repositories;

public class PseudonimoRepository : IPseudonimoRepository
{
    private readonly AppDbContext _context;
    public PseudonimoRepository(AppDbContext context)
    {
        _context = context;
    }

    
    public IEnumerable<Pseudonimo> Pseudonimos => _context.Pseudonimo;
    public IEnumerable<Autor> Autores => _context.Autor;

    public async Task AddAsync(Pseudonimo pseudonimo)
    {
        _context.Add(pseudonimo);
        await _context.SaveChangesAsync();
    }
    public async Task<Pseudonimo?> GetByIdAsync(int? id)
    {
        return await _context.Pseudonimo.FindAsync(id);
    }

    public async Task UpdatePseudonimo(Pseudonimo pseudonimo)
    {
        _context.Pseudonimo.Update(pseudonimo);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaPseudonimo(Pseudonimo pseudonimo)
    {
        _context.Pseudonimo.Remove(pseudonimo);
        await _context.SaveChangesAsync();
    }

    public async Task<Autor?> GetAutorByIdAsync(int? id)
    {
        return await _context.Autor.FindAsync(id);
    }
}