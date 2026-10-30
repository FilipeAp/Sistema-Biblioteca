using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Componente;

public class PessoaMenu : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
