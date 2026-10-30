using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces;

public interface ILivroRepository
{
    IEnumerable<Livro> Livros { get; }

    IEnumerable<Categoria> Categorias { get; }

    IEnumerable<Subcategoria> Subcategorias { get; }

    IEnumerable<Editora> Editoras { get; }

    IEnumerable<Autor> Autores { get; }

    IEnumerable<Emprestimo> Emprestimos { get; }

    Task AddAsync(
    Livro livro,
    IEnumerable<int> autoresIds,
    IEnumerable<int> editorasIds);

    Task<Livro?> GetByIdAsync(int? id);

    Task UpdateLivro(Livro livro, IEnumerable<int> autoresIds, IEnumerable<int> editorasIds);
    Task DeletaLivro(Livro livro);
}
