
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;
using Biblioteca.Context;
using Biblioteca.Repositories.Interfaces;

public class EditoraController : Controller
{
    private readonly IEditoraRepository _editoraRepository;

    public EditoraController(IEditoraRepository editoraRepository)
    {
        _editoraRepository = editoraRepository;
    }

    // GET: EDITORAS
    public async Task<IActionResult> Index()    
    {
        return View(_editoraRepository.Editoras.ToList());
    }

    // GET: EDITORAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var editora = await _editoraRepository.GetByIdAsync(id);
        if (editora == null)
        {
            return NotFound();
        }

        return View(editora);
    }

    // GET: EDITORAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: EDITORAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EditoraId,NomeEditora,Telefone,Email,CnpjEditora,CnpjMatriz,Logradouro,Numero,Bairro,Cidade,Cep,Complemento")] Editora editora)
    {
        if (ModelState.IsValid)
        {
            await _editoraRepository.AddAsync(editora);
            return RedirectToAction(nameof(Index));
        }
        return View(editora);
    }

    // GET: EDITORAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var editora = await _editoraRepository.GetByIdAsync(id);
        if (editora == null)
        {
            return NotFound();
        }
        return View(editora);
    }

    // POST: EDITORAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("EditoraId,NomeEditora,Telefone,Email,CnpjEditora,CnpjMatriz,Logradouro,Numero,Bairro,Cidade,Cep,Complemento")] Editora editora)
    {
        if (id != editora.EditoraId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _editoraRepository.UpdateEditora(editora);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditoraExists(editora.EditoraId))
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
        return View(editora);
    }

    // GET: EDITORAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var editora = await _editoraRepository.GetByIdAsync(id);
        if (editora == null)
        {
            return NotFound();
        }

        return View(editora);
    }

    // POST: EDITORAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var editora = await _editoraRepository.GetByIdAsync(id);
        if (editora != null)
        {
            await _editoraRepository.DeletaEditora(editora);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool EditoraExists(int? id)
    {
        return _editoraRepository.Editoras.Any(e => e.EditoraId == id);
    }
}
