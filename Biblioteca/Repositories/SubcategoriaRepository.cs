using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;

namespace Biblioteca.Repositories;

public class SubcategoriaRepository : ISubcategoriaRepository
{
    private readonly AppDbContext _context;
    public SubcategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Subcategoria> Subcategorias => _context.Subcategoria;

    public async Task<Subcategoria?> GetByIdAsync(int? id)
    {
        return await _context.Subcategoria.FindAsync(id);
    }

    public async Task CriarSubcategoria(Subcategoria subcategoria)
    {
        _context.Subcategoria.Add(subcategoria);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSubcategoria(Subcategoria subcategoria)
    {
        _context.Subcategoria.Update(subcategoria);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaSubcategoria(Subcategoria subcategoria)
    {
        _context.Subcategoria.Remove(subcategoria);
        await _context.SaveChangesAsync();
    }
}
