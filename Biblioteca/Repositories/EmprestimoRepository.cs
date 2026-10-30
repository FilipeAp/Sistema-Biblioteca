using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly AppDbContext _context;

    public EmprestimoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Emprestimo> Emprestimos => _context.Emprestimo.Include(u => u.Usuario);
    public IEnumerable<Copia> Copias => _context.Copia.Include(c => c.Livro);
    public IEnumerable<Funcionario> Funcionarios => _context.Funcionario;
    public IEnumerable<Usuario> Usuarios => _context.Usuario;

    public async Task RegistrarDevolucaoAsync(Emprestimo emprestimo)
    {
        foreach(var copia in emprestimo.Copias)
        {
            copia.Disponivel = true;
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Emprestimo emprestimo,
        IEnumerable<int> copiasIds)
    {
        var idsCopias = copiasIds.Distinct().ToList();

        var copias = await _context.Copia
            .Where(a => idsCopias.Contains(a.CopiaId))
            .ToListAsync();


        // Não permita associar IDs inexistentes.
        if (copias.Count != idsCopias.Count)
        {
            throw new ArgumentException(
                "Uma ou mais copias não foram encontradas.");
        }

        emprestimo.Copias = copias;
        
        //pegar cada copia e atualizar o status de disponibilidade deçlas 01

        foreach(var copia in copias)
        {
            if (copia.Disponivel == false)
            {
                throw new ArgumentException(
                    "Uma ou mais copias não estão disponíveis.");
            }
            copia.Disponivel = false;
            _context.Copia.Update(copia);
        }

        _context.Emprestimo.Add(emprestimo);
        await _context.SaveChangesAsync();
    }




    public async Task<Emprestimo?> GetByIdAsync(int? id)
    {
        return await _context.Emprestimo
            .Include(e => e.Copias).ThenInclude(l => l.Livro)
            .Include(e => e.Usuario)
            .Include(e => e.Funcionario)
            .FirstOrDefaultAsync(e => e.EmprestimoId == id);
    }

    public async Task UpdateEmprestimo(Emprestimo emprestimo, IEnumerable<int> copiasIds)
    {
        var idsCopias = copiasIds.Distinct().ToList();

        var copias = await _context.Copia.Where(c => idsCopias.Contains(c.CopiaId)).ToListAsync();

        if (idsCopias.Count != copias.Count)
        {
            throw new ArgumentException(
                "Uma ou mais copias não foram encontradas.");
        }

        emprestimo.Copias = copias;

        foreach(var copia in copias)
        {
            if (copia.Disponivel == false)
            {
                throw new ArgumentException(
                    "Uma ou mais copias não estão disponíveis.");
            }
            copia.Disponivel = false;
            _context.Copia.Update(copia);
        }

        _context.Emprestimo.Update(emprestimo);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaEmprestimo(Emprestimo emprestimo)
    {
        _context.Emprestimo.Remove(emprestimo);
        await _context.SaveChangesAsync();
    }
}
