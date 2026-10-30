
using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class CopiaController : Controller
{
    private readonly ICopiaRepository _copiaRepository;

    public CopiaController(ICopiaRepository copiaRepository)
    {
        _copiaRepository = copiaRepository;
    }

    // GET: COPIAS
    public async Task<IActionResult> Index()    
    {
        return View(_copiaRepository.Copias.ToList());
    }

    //private void CarregaOpcoes(CopiaViewModel vm)
    //{
    //    vm.Estados = _copiaRepository.Copias.Select(c => new SelectListItem
    //    {
    //        Value = c.CopiaId
    //    })
    //        vm.Categorias = _livroRepository.Categorias.Select(c =>
    //        new SelectListItem
    //        {
    //            Value = c.CategoriaId.ToString(),
    //            Text = c.NomeCategoria
    //        }).ToList();
    //}

    // GET: COPIAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var copia = await _copiaRepository.GetByIdAsync(id);
        //ViewBag.Emprestimo = _copiaRepository.Emprestimos.Where(e => e.CopiaId == id || copia.Disponivel = false);
        if (copia == null)
        {
            return NotFound();
        }
        ViewBag.Livro = copia.Livro.Titulo;
        return View(copia);
    }

    // GET: COPIAS/Create
    public IActionResult Create()
    {
        ViewBag.Livros = _copiaRepository.Livros.ToList();

        return View();
    }

    // POST: COPIAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LivroId,EstadoConservacao,Disponivel")] Copia copia)
    {
        var livro = await _copiaRepository.GetByIdAsyncLivro(copia.LivroId);

        if (livro == null)
        {
            ModelState.AddModelError(
                "LivroId",
                "O livro selecionado não foi encontrado."
            );
        }
        else
        {
            copia.Livro = livro;
        }


        if (ModelState.IsValid)
        {
            await _copiaRepository.AddAsync(copia);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Livros = _copiaRepository.Livros.ToList();
        return View(copia);
    }

    // GET: COPIAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var copia = await _copiaRepository.GetByIdAsync(id);
        if (copia == null)
        {
            return NotFound();
        }
        ViewBag.Livros = _copiaRepository.Livros.ToList();
        return View(copia);
    }

    // POST: COPIAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("CopiaId,LivroId,EstadoConservacao,Disponivel,Livro")] Copia copia)
    {
        if (id != copia.CopiaId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _copiaRepository.UpdateCopia(copia);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CopiaExists(copia.CopiaId))
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
        return View(copia);
    }

    // GET: COPIAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var copia = await _copiaRepository.GetByIdAsync(id);
        if (copia == null)
        {
            return NotFound();
        }
        ViewBag.Livro = copia.Livro.Titulo;
        return View(copia);
    }

    // POST: COPIAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var copia = await _copiaRepository.GetByIdAsync(id);
        if (copia != null)
        {
            await _copiaRepository.DeletaCopia(copia);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool CopiaExists(int? id)
    {
        return _copiaRepository.Copias.Any(e => e.CopiaId == id);
    }
}
