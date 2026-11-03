
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class LivroController : Controller
{
    private readonly ILivroRepository _livroRepository;

    public LivroController(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    // GET: LIVROS
    public async Task<IActionResult> Index()    
    {
        return View(_livroRepository.Livros.ToList());
    }

    // GET: LIVROS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _livroRepository.GetByIdAsync(id);
        if (livro == null)
        {
            return NotFound();
        }
        ViewBag.Emprestimo = _livroRepository.Emprestimos;
        return View(livro);
    }


    private void CarregarOpcoes(LivroViewModel vm)
    {
        vm.Categorias = _livroRepository.Categorias.Select(c =>
            new SelectListItem
            {
                Value = c.CategoriaId.ToString(),
                Text = c.NomeCategoria
            }).ToList();

        vm.Subcategorias = _livroRepository.Subcategorias.Select(s =>
            new SelectListItem
            {
                Value = s.SubcategoriaId.ToString(),
                Text = s.NomeSubcategoria
            }).ToList();

        vm.Autores = _livroRepository.Autores.Select(a =>
            new SelectListItem
            {
                Value = a.AutorId.ToString(),
                Text = a.NomeAutor
            }).ToList();

        vm.Editoras = _livroRepository.Editoras.Select(e =>
            new SelectListItem
            {
                Value = e.EditoraId.ToString(),
                Text = e.NomeEditora
            }).ToList();
    }

    // GET: LIVROS/Create

    [HttpGet]
    public IActionResult Create()
    {
        var vm = new LivroViewModel();

        CarregarOpcoes(vm);

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LivroViewModel vm)
    {
        if (vm.AutoresIds.Count == 0)
        {
            ModelState.AddModelError(
                nameof(vm.AutoresIds),
                "Selecione pelo menos um autor.");
        }

        if (vm.EditorasIds.Count == 0)
        {
            ModelState.AddModelError(
                nameof(vm.EditorasIds),
                "Selecione pelo menos uma editora.");
        }

        if (!ModelState.IsValid)
        {
            CarregarOpcoes(vm);
            return View(vm);
        }

        var livro = new Livro
        {
            Titulo = vm.Titulo,
            ISBN = vm.ISBN,
            Ano_Publicado = vm.Ano_Publicado,
            CategoriaId = vm.CategoriaId!.Value,
            SubcategoriaId = vm.SubcategoriaId!.Value
        };

        try
        {
            await _livroRepository.AddAsync(
                livro,
                vm.AutoresIds,
                vm.EditorasIds);
        }
        catch (ArgumentException)
        {
            ModelState.AddModelError(
                string.Empty,
                "Um ou mais autores ou editoras selecionados são inválidos.");

            CarregarOpcoes(vm);
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }


    // GET: LIVROS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _livroRepository.GetByIdAsync(id);

        if (livro == null)
        {
            return NotFound();
        }

        var vmLivro = new LivroViewModel()
        {
            LivroId = livro.LivroId,
            Titulo = livro.Titulo,
            ISBN = livro.ISBN,
            Ano_Publicado = livro.Ano_Publicado,
            CategoriaId = livro.CategoriaId,
            SubcategoriaId = livro.SubcategoriaId,
            AutoresIds = livro.Autores.Select(a => a.AutorId).ToList(),
            EditorasIds = livro.Editoras.Select(e => e.EditoraId).ToList()
        };


        CarregarOpcoes(vmLivro);

        return View(vmLivro);
    }
    //Talvez colocar o view model para receber infromações do livro, enviar o view model pra view no envio da view trazer o view model

    // POST: LIVROS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, LivroViewModel vmLivro)
    {
        if (id == null || id != vmLivro.LivroId)
        {
            return NotFound();
        }

        if (vmLivro.AutoresIds.Count == 0)
        {
            ModelState.AddModelError(
                nameof(vmLivro.AutoresIds),
                "Selecione pelo menos um autor.");
        }

        if (vmLivro.EditorasIds.Count == 0)
        {
            ModelState.AddModelError(
                nameof(vmLivro.EditorasIds),
                "Selecione pelo menos uma editora.");
        }

        if (ModelState.IsValid)
        {
            var livro = await _livroRepository.GetByIdAsync(vmLivro.LivroId);

            livro.Titulo = vmLivro.Titulo;
            livro.ISBN = vmLivro.ISBN;
            livro.Ano_Publicado = vmLivro.Ano_Publicado;
            livro.CategoriaId = vmLivro.CategoriaId!.Value;
            livro.SubcategoriaId = vmLivro.SubcategoriaId!.Value;


            try
            {
                await _livroRepository.UpdateLivro(livro, vmLivro.AutoresIds, vmLivro.EditorasIds);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(livro.LivroId))
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

        CarregarOpcoes(vmLivro);
        return View(vmLivro);
    }

    // GET: LIVROS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _livroRepository.GetByIdAsync(id);
        if (livro == null)
        {
            return NotFound();
        }

        return View(livro);
    }

    // POST: LIVROS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var livro = await _livroRepository.GetByIdAsync(id);
        if (livro != null)
        {
            await _livroRepository.DeletaLivro(livro);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool LivroExists(int? id)
    {
        return _livroRepository.Livros.Any(e => e.LivroId == id);
    }
}
