
using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.ViewModel;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class EmprestimoController : Controller
{
    private readonly IEmprestimoRepository _emprestimoRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;

    public EmprestimoController(IEmprestimoRepository emprestimoRepository, IFuncionarioRepository funcionarioRepository)
    {
        _emprestimoRepository = emprestimoRepository;
        _funcionarioRepository = funcionarioRepository;
    }

    // GET: EMPRESTIMOS
    public async Task<IActionResult> Index() 
    {
        var emprestimos = _emprestimoRepository.Emprestimos
        .OrderByDescending(e => e.DataRetida)
        .ToList();

        return View(emprestimos);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegistrarEntrega(int id)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(id);

        if (emprestimo == null)
        {
            return NotFound();
        }

        if (emprestimo.DataEntregue != null)
        {
            return RedirectToAction(nameof(Index));
        }

        var dataEntrega = DateTime.Now;

        // Calcula somente os dias completos de atraso
        // considerando as datas, sem os horários.
        var dataEntregaSomenteData = DateOnly.FromDateTime(dataEntrega);

        var diasAtraso = Math.Max(
            0,
            (
                dataEntregaSomenteData.ToDateTime(TimeOnly.MinValue)
                - emprestimo.PrevisaoDevolucao.ToDateTime(TimeOnly.MinValue)
            ).Days
        );

        const decimal multaPorDia = 2.00m;

        emprestimo.DataEntregue = dataEntrega;
        emprestimo.Multa = diasAtraso * multaPorDia;

        // Salva as alterações no banco.
        await _emprestimoRepository.RegistrarDevolucaoAsync(emprestimo);

        return RedirectToAction(nameof(Index));
    }


    // GET: EMPRESTIMOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var emprestimo = await _emprestimoRepository.GetByIdAsync(id);
        if (emprestimo == null)
        {
            return NotFound();
        }

        return View(emprestimo);
    }
    
    private async Task CarregarOpcoes(EmprestimoViewModel vm)
    {
        var FuncionarioAutenticado = await _funcionarioRepository.GetByIdentityUserIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        vm.Copias = _emprestimoRepository.Copias.Where(c => c.Disponivel == true).Select(c =>
            new SelectListItem
            {
                Value = c.CopiaId.ToString(),
                Text = $"Cópia {c.CopiaId} - {c.Livro.Titulo}"
            }).ToList();

        vm.Usuarios = _emprestimoRepository.Usuarios.Select(u =>
            new SelectListItem
            {
                Value = u.UsuarioId.ToString(),
                Text = u.Nome
            }).ToList();

        //vm.Funcionarios = _emprestimoRepository.Funcionarios.Select(f =>
        //    new SelectListItem
        //    {
        //        Value = f.FuncionarioId.ToString(),
        //        Text = f.Email
        //    }).ToList();

        vm.NomeFuncionario = FuncionarioAutenticado.Nome;
        vm.EmailFuncionario = FuncionarioAutenticado.Email;
    }

    // GET: EMPRESTIMOS/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var vm = new EmprestimoViewModel();

        await CarregarOpcoes(vm);

        return View(vm);
    }

    // POST: EMPRESTIMOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmprestimoViewModel vm)
    {
        var FuncionarioAutenticado = await _funcionarioRepository.GetByIdentityUserIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if (vm.CopiaIds.Count == 0)
        {
            ModelState.AddModelError(
                nameof(vm.CopiaIds),
                "Selecione pelo menos uma cópia.");
        }


        if (!ModelState.IsValid)
        {
            CarregarOpcoes(vm);
            return View(vm);
        }

        var emprestimo = new Emprestimo
        {
            DataRetida = vm.DataRetida,
            PrevisaoDevolucao = vm.PrevisaoDevolucao,
            UsuarioId = vm.UsuarioId!.Value,
            FuncionarioId = FuncionarioAutenticado.FuncionarioId
        };

        try
        {
            await _emprestimoRepository.AddAsync(
                emprestimo,
                vm.CopiaIds);
        }
        catch (ArgumentException)
        {
            ModelState.AddModelError(
                string.Empty,
                "Uma ou mais cópias selecionadas são inválidas.");

            CarregarOpcoes(vm);
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: EMPRESTIMOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var emprestimo = await _emprestimoRepository.GetByIdAsync(id);
        if (emprestimo == null)
        {
            return NotFound();
        }

        return View(emprestimo);
    }

    // POST: EMPRESTIMOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(id);
        if (emprestimo != null)
        {
            await _emprestimoRepository.RegistrarDevolucaoAsync(emprestimo);
            await _emprestimoRepository.DeletaEmprestimo(emprestimo);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool EmprestimoExists(int? id)
    {
        return _emprestimoRepository.Emprestimos.Any(e => e.EmprestimoId == id);
    }
}
