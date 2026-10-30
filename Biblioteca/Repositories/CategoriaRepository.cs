using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> Categorias => _context.Categoria;

    public async Task AddAsync(Categoria categoria)
    {
        _context.Add(categoria);
        await _context.SaveChangesAsync();
    }
    public async Task<Categoria?> GetByIdAsync(int? id)
    {
        return await _context.Categoria.FindAsync(id);
    }

    public async Task UpdateCategoria(Categoria categoria)
    {
        _context.Categoria.Update(categoria);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaCategoria(Categoria categoria)
    {
        _context.Categoria.Remove(categoria);
        await _context.SaveChangesAsync();
    }
}
