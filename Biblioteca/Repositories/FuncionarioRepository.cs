using Biblioteca.Context;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Models;

namespace Biblioteca.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly AppDbContext _context;
    public FuncionarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Funcionario> Funcionarios => _context.Funcionario;
    public async Task AddAsync(Funcionario funcionario)
    {
        _context.Add(funcionario);
        await _context.SaveChangesAsync();
    }
    public async Task<Funcionario?> GetByIdAsync(int? id)
    {
        return await _context.Funcionario.FindAsync(id);
    }

    public async Task UpdateFuncionario(Funcionario funcionario)
    {
        _context.Funcionario.Update(funcionario);
        await _context.SaveChangesAsync();
    }
    public async Task DeletaFuncionario(Funcionario funcionario)
    {
        _context.Funcionario.Remove(funcionario);
        await _context.SaveChangesAsync();
    }

}
