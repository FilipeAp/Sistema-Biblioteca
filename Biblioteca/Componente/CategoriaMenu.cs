using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Componente;

public class CategoriaMenu : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
