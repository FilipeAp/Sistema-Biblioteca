
using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class SubcategoriaController : Controller
{
    private readonly ISubcategoriaRepository _subcategoriaRepository;

    public SubcategoriaController(ISubcategoriaRepository subcategoriaRepository)
    {
        _subcategoriaRepository = subcategoriaRepository;
    }

    // GET: SUBCATEGORIAS
    public async Task<IActionResult> Index()
    {
        return View(_subcategoriaRepository.Subcategorias.ToList());
    }

    // GET: SUBCATEGORIAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subcategoria = await _subcategoriaRepository.GetByIdAsync(id);
        if (subcategoria == null)
        {
            return NotFound();
        }

        return View(subcategoria);
    }

    // GET: SUBCATEGORIAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SUBCATEGORIAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SubcategoriaId,NomeSubcategoria")] Subcategoria subcategoria)
    {
        if (ModelState.IsValid)
        {
            await _subcategoriaRepository.CriarSubcategoria(subcategoria);
            return RedirectToAction(nameof(Index));
        }
        return View(subcategoria);
    }

    // GET: SUBCATEGORIAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subcategoria = await _subcategoriaRepository.GetByIdAsync(id);
        if (subcategoria == null)
        {
            return NotFound();
        }
        return View(subcategoria);
    }

    // POST: SUBCATEGORIAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("SubcategoriaId,NomeSubcategoria")] Subcategoria subcategoria)
    {
        if (id != subcategoria.SubcategoriaId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _subcategoriaRepository.UpdateSubcategoria(subcategoria);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcategoriaExists(subcategoria.SubcategoriaId))
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
        return View(subcategoria);
    }

    // GET: SUBCATEGORIAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subcategoria = await _subcategoriaRepository.GetByIdAsync(id);
        if (subcategoria == null)
        {
            return NotFound();
        }

        return View(subcategoria);
    }

    // POST: SUBCATEGORIAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var subcategoria = await _subcategoriaRepository.GetByIdAsync(id);
        if (subcategoria != null)
        {
            await _subcategoriaRepository.DeletaSubcategoria(subcategoria);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool SubcategoriaExists(int? id)
    {
        return _subcategoriaRepository.Subcategorias.Any(e => e.SubcategoriaId == id);
    }
}
