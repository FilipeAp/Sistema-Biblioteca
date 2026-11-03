
using Biblioteca.Context;
using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class AutorController : Controller
{
    private readonly IAutorRepository _autorRepository;

    public AutorController(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    // GET: AUTORS
    public async Task<IActionResult> Index()    
    {
        return View(_autorRepository.Autores);
    }

    // GET: AUTORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var autor = await _autorRepository.GetByIdAsync(id);
        if (autor == null)
        {
            return NotFound();
        }

        return View(autor);
    }

    // GET: AUTORS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: AUTORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AutorId,NomeAutor,Pseudonimos,Livros")] Autor autor)
    {
        if (ModelState.IsValid)
        {
            await _autorRepository.AddAsync(autor);
            return RedirectToAction(nameof(Index));
        }
        return View(autor);
    }

    // GET: AUTORS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var autor = await _autorRepository.GetByIdAsync(id);
        if (autor == null)
        {
            return NotFound();
        }
        return View(autor);
    }

    // POST: AUTORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("AutorId,NomeAutor")] Autor autor)
    {
        if (id != autor.AutorId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _autorRepository.UpdateAutor(autor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(autor.AutorId))
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
        return View(autor);
    }

    // GET: AUTORS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var autor = await _autorRepository.GetByIdAsync(id);
        if (autor == null)
        {
            return NotFound();
        }

        return View(autor);
    }

    // POST: AUTORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var autor = await _autorRepository.GetByIdAsync(id);
        if (autor != null)
        {
            await _autorRepository.DeletaAutor(autor);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool AutorExists(int? id)
    {
        return _autorRepository.Autores.Any(e => e.AutorId == id);
    }
}
