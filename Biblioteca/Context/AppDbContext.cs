using Biblioteca.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Context;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Autor> Autor { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Copia> Copia { get; set; }
    public DbSet<Editora> Editora { get; set; }
    public DbSet<Emprestimo> Emprestimo { get; set; }
    public DbSet<Funcionario> Funcionario { get; set; }
    public DbSet<Livro> Livro { get; set; }
    public DbSet<Pseudonimo> Pseudonimo { get; set; }
    public DbSet<Subcategoria> Subcategoria { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
}
