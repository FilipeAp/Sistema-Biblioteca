
using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class FuncionarioController : Controller
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FuncionarioController(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    // GET: FUNCIONARIOS
    public async Task<IActionResult> Index()    
    {
        return View(_funcionarioRepository.Funcionarios.ToList());
    }

    // GET: FUNCIONARIOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionario = await _funcionarioRepository.GetByIdAsync(id);
        if (funcionario == null)
        {
            return NotFound();
        }

        return View(funcionario);
    }

    // GET: FUNCIONARIOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FUNCIONARIOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FuncionarioId,Cpf,Nome,Telefone,Email,Logradouro,Numero,Bairro,Cidade,Cep,Complemento,Emprestimos")] Funcionario funcionario)
    {
        if (ModelState.IsValid)
        {
            await _funcionarioRepository.AddAsync(funcionario);
            return RedirectToAction(nameof(Index));
        }
        return View(funcionario);
    }

    // GET: FUNCIONARIOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionario = await _funcionarioRepository.GetByIdAsync(id);
        if (funcionario == null)
        {
            return NotFound();
        }
        return View(funcionario);
    }

    // POST: FUNCIONARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("FuncionarioId,Cpf,Nome,Telefone,Email,Logradouro,Numero,Bairro,Cidade,Cep,Complemento,Emprestimos")] Funcionario funcionario)
    {
        if (id != funcionario.FuncionarioId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _funcionarioRepository.UpdateFuncionario(funcionario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(funcionario.FuncionarioId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(funcionario);
    }

    // GET: FUNCIONARIOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var funcionario = await _funcionarioRepository.GetByIdAsync(id);
        if (funcionario == null)
        {
            return NotFound();
        }

        return View(funcionario);
    }

    // POST: FUNCIONARIOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var funcionario = await _funcionarioRepository.GetByIdAsync(id);
        if (funcionario != null)
        {
            await _funcionarioRepository.DeletaFuncionario(funcionario);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool FuncionarioExists(int? id)
    {
        return _funcionarioRepository.Funcionarios.Any(e => e.FuncionarioId == id);
    }
}
