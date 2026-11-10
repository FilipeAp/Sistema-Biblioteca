using Biblioteca.Repositories.Interfaces;
using System.Security.Claims;

namespace Biblioteca.Middleware;

public class VerificarFuncionarioMiddleware
{
    private readonly RequestDelegate _next;

    public VerificarFuncionarioMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IFuncionarioRepository funcionarioRepository)
    {
        // Verifica se o usuário está autenticado.
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var caminho = context.Request.Path;

            // Rotas que precisam continuar acessíveis.
            var rotaPermitida =
                caminho.StartsWithSegments("/Acesso/Pendente") ||
                caminho.StartsWithSegments("/Account/Logout");

            if (rotaPermitida)
            {
                await _next(context);
                return;
            }

            // Obtém o ID do usuário autenticado no Identity.
            var identityUserId = context.User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(identityUserId))
            {
                context.Response.Redirect("/Acesso/Pendente");
                return;
            }

            // Verifica se existe um funcionário associado.
            var possuiFuncionario = await funcionarioRepository
                .ExisteVinculoAsync(identityUserId);


            if (!possuiFuncionario)
            {
                context.Response.Redirect("/Acesso/Pendente");
                return;
            }
        }

        await _next(context);
    }
}

