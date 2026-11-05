using Biblioteca.Context;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddTransient<IAutorRepository, AutorRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IEditoraRepository, EditoraRepository>();
builder.Services.AddTransient<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddTransient<ILivroRepository, LivroRepository>();
builder.Services.AddTransient<IPseudonimoRepository, PseudonimoRepository>();
builder.Services.AddTransient<ISubcategoriaRepository, SubcategoriaRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<ICopiaRepository, CopiaRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

var app = builder.Build();

// Inicialização das Roles
using (var scope = app.Services.CreateScope())
{
    var seedRoleInitial = scope.ServiceProvider
        .GetRequiredService<ISeedUserRoleInitial>();

    seedRoleInitial.SeedRoles();
    seedRoleInitial.SeedUsers();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Emprestimo}/{action=Index}/{id?}");

app.Run();
