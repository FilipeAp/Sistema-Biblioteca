//IMPLEMENTAÇÃO FUTURA

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Biblioteca.Models;
//using Biblioteca.Context;
//using Biblioteca.Repositories.Interfaces;

//public class PseudonimoController : Controller
//{
//    private readonly IPseudonimoRepository _pseudonimoRepository;

//    public PseudonimoController(IPseudonimoRepository pseudonimoRepository)
//    {
//        _pseudonimoRepository = pseudonimoRepository;
//    }
//    private readonly IAutorRepository _autorRepository;

//    // GET: PSEUDONIMOS
//    public async Task<IActionResult> Index()    
//    {
//        return View(_pseudonimoRepository.Pseudonimos);
//    }

//    // GET: PSEUDONIMOS/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var pseudonimo = await _pseudonimoRepository.GetByIdAsync(id);
//        if (pseudonimo == null)
//        {
//            return NotFound();
//        }

//        return View(pseudonimo);
//    }

//    // GET: PSEUDONIMOS/Create
//    public async Task<IActionResult> Create(int? id)
//    {
//        ViewData["AutorId"] = id;
//        ViewData["Autor"] = await _pseudonimoRepository.GetAutorByIdAsync(id);
//        return View();
//    }

//    // POST: PSEUDONIMOS/Create
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create([Bind("PseudonimoId,NomePseudonimo, AutorId, Autor")] Pseudonimo pseudonimo)
//    {
        
//        if (ModelState.IsValid)
//        {
//            await _pseudonimoRepository.AddAsync(pseudonimo);
//            return RedirectToAction("Index", "Autor");
//        }
//        return View(pseudonimo);
//    }

//    // GET: PSEUDONIMOS/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var pseudonimo = await _pseudonimoRepository.GetByIdAsync(id);
//        if (pseudonimo == null)
//        {
//            return NotFound();
//        }
//        return View(pseudonimo);
//    }

//    // POST: PSEUDONIMOS/Edit/5
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int? id, [Bind("PseudonimoId, NomePseudonimo, AutorId")] Pseudonimo pseudonimo)
//    {
//        if (id != pseudonimo.PseudonimoId)
//        {
//            return NotFound();
//        }

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                await _pseudonimoRepository.UpdatePseudonimo(pseudonimo);
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!PseudonimoExists(pseudonimo.PseudonimoId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//            return RedirectToAction("Index", "Autor");
//        }
//        return View(pseudonimo);
//    }

//    // GET: PSEUDONIMOS/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var pseudonimo = await _pseudonimoRepository.GetByIdAsync(id);
//        if (pseudonimo == null)
//        {
//            return NotFound();
//        }

//        return View(pseudonimo);
//    }

//    // POST: PSEUDONIMOS/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int? id)
//    {
//        var pseudonimo = await _pseudonimoRepository.GetByIdAsync(id);
//        if (pseudonimo != null)
//        {
//            await _pseudonimoRepository.DeletaPseudonimo(pseudonimo);
//        }

//        return RedirectToAction("Index", "Autor");
//    }

//    private bool PseudonimoExists(int? id)
//    {
//        return _pseudonimoRepository.Pseudonimos.Any(e => e.PseudonimoId == id);
//    }
//}
