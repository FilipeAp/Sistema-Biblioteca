using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers;

public class AcessoController : Controller
{
    [HttpGet]
    public IActionResult Pendente()
    {
        return View();
    }
}
