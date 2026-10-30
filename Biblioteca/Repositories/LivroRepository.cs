using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly AppDbContext _context;

    public LivroRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Livro> Livros => _context.Livro.Include(c => c.Copias);
    public IEnumerable<Categoria> Categorias => _context.Categoria;
    public IEnumerable<Subcategoria> Subcategorias => _context.Subcategoria;
    public IEnumerable<Editora> Editoras => _context.Editora;
    public IEnumerable<Autor> Autores => _context.Autor;
    public IEnumerable<Emprestimo> Emprestimos => _context.Emprestimo.Include(c => c.Copias);

    public async Task AddAsync(
        Livro livro,
        IEnumerable<int> autoresIds,
        IEnumerable<int> editorasIds)
    {
        var idsAutores = autoresIds.Distinct().ToList();
        var idsEditoras = editorasIds.Distinct().ToList();

        var autores = await _context.Autor
            .Where(a => idsAutores.Contains(a.AutorId))
            .ToListAsync();

        var editoras = await _context.Editora
            .Where(e => idsEditoras.Contains(e.EditoraId))
            .ToListAsync();

        // Não permita associar IDs inexistentes.
        if (autores.Count != idsAutores.Count ||
            editoras.Count != idsEditoras.Count)
        {
            throw new ArgumentException(
                "Um ou mais autores ou editoras não foram encontrados.");
        }

        livro.Autores = autores;
        livro.Editoras = editoras;

        _context.Livro.Add(livro);

        await _context.SaveChangesAsync();
    }

    public async Task<Livro?> GetByIdAsync(int? id)
    {
        return await _context.Livro
            .Include(c => c.Copias)
            .Include(l => l.Editoras)
            .Include(l => l.Autores)
            .Include(l => l.Subcategoria)
            .Include(l => l.Categoria)
            .FirstOrDefaultAsync(l => l.LivroId == id);
    }

    public async Task UpdateLivro(Livro livro, IEnumerable<int> autoresIds, IEnumerable<int> editorasIds)
    {
        var idsAutores = autoresIds.Distinct().ToList();
        var idsEditoras = editorasIds.Distinct().ToList();

        var autores = await _context.Autor.Where(a => idsAutores.Contains(a.AutorId)).ToListAsync();
        var editoras = await _context.Editora.Where(e => idsEditoras.Contains(e.EditoraId)).ToListAsync();

        if (idsAutores.Count != autores.Count ||  idsEditoras.Count != editoras.Count)
        {
            throw new ArgumentException(
                "Um ou mais autores ou editoras não foram encontrados.");
        }

        livro.Autores = autores;
        livro.Editoras = editoras;

        _context.Livro.Update(livro);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaLivro(Livro livro)
    {
        _context.Livro.Remove(livro);
        await _context.SaveChangesAsync();
    }

}
