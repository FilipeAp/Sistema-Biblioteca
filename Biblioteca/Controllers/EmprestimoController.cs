
using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.ViewModel;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class EmprestimoController : Controller
{
    private readonly IEmprestimoRepository _emprestimoRepository;

    public EmprestimoController(IEmprestimoRepository emprestimoRepository)
    {
        _emprestimoRepository = emprestimoRepository;
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

    private void CarregarOpcoes(EmprestimoViewModel vm)
    {
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

        vm.Funcionarios = _emprestimoRepository.Funcionarios.Select(f =>
            new SelectListItem
            {
                Value = f.FuncionarioId.ToString(),
                Text = f.Nome
            }).ToList();
    }

    // GET: EMPRESTIMOS/Create
    [HttpGet]
    public IActionResult Create()
    {
        var vm = new EmprestimoViewModel();

        CarregarOpcoes(vm);

        return View(vm);
    }

    // POST: EMPRESTIMOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmprestimoViewModel vm)
    {

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
            FuncionarioId = vm.FuncionarioId!.Value
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

    // GET: EMPRESTIMOS/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
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

        var vmEmprestimo = new EmprestimoViewModel()
        {
            EmprestimoId = emprestimo.EmprestimoId,
            DataRetida = emprestimo.DataRetida,
            PrevisaoDevolucao = emprestimo.PrevisaoDevolucao,
            UsuarioId = emprestimo.UsuarioId,
            FuncionarioId = emprestimo.FuncionarioId,
            CopiaIds = emprestimo.Copias.Select(c => c.CopiaId).ToList()
        };


        CarregarOpcoes(vmEmprestimo);

        return View(vmEmprestimo);
    }

    // POST: EMPRESTIMOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, EmprestimoViewModel vmEmprestimo)
    {
        if (id == null|| id != vmEmprestimo.EmprestimoId)
        {
            return NotFound();
        }

        if (vmEmprestimo.CopiaIds.Count == 0)
        {
            ModelState.AddModelError(
                nameof(vmEmprestimo.CopiaIds),
                "Selecione pelo menos uma cópia.");
        }

        if (ModelState.IsValid)
        {
            var emprestimo = await _emprestimoRepository.GetByIdAsync(vmEmprestimo.EmprestimoId);

            emprestimo.DataRetida = vmEmprestimo.DataRetida;
            emprestimo.PrevisaoDevolucao = vmEmprestimo.PrevisaoDevolucao;
            emprestimo.UsuarioId = vmEmprestimo.UsuarioId!.Value;
            emprestimo.FuncionarioId = vmEmprestimo.FuncionarioId!.Value;

            try
            {
                await _emprestimoRepository.UpdateEmprestimo(emprestimo, vmEmprestimo.CopiaIds);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmprestimoExists(emprestimo.EmprestimoId))
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
        CarregarOpcoes(vmEmprestimo);
        return View(vmEmprestimo);
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
