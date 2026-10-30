using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories;

public class CopiaRepository : ICopiaRepository
{
    private readonly AppDbContext _context;

    public CopiaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Copia> Copias => _context.Copia.Include(c => c.Livro);
    public IEnumerable<Livro> Livros => _context.Livro;
    public IEnumerable<Emprestimo> Emprestimos => _context.Emprestimo;

    public async Task AddAsync(Copia copia)
    {
        _context.Add(copia);
        await _context.SaveChangesAsync();
    }
    public async Task<Copia?> GetByIdAsync(int? id)
    {
        return await _context.Copia.Include(c => c.Livro).FirstOrDefaultAsync(c => c.CopiaId ==id);
    }
    public async Task<Livro?> GetByIdAsyncLivro(int? id)
    {
        return await _context.Livro.FindAsync(id);
    }

    public async Task UpdateCopia(Copia copia)
    {
        _context.Copia.Update(copia);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaCopia(Copia copia)
    {
        _context.Copia.Remove(copia);
        await _context.SaveChangesAsync();
    }
}
