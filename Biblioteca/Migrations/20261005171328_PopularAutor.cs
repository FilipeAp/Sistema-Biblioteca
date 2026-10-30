using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class PopularAutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Autor(NomeAutor) VALUES('Eric Arthur Blair')");
            migrationBuilder.Sql("INSERT INTO Autor(NomeAutor) VALUES('Samuel Langhorne Clemens')");
            migrationBuilder.Sql("INSERT INTO Autor(NomeAutor) VALUES('Mary Ann Evans')");
            migrationBuilder.Sql("INSERT INTO Autor(NomeAutor) VALUES('Charles Lutwidge Dodgson')");
            migrationBuilder.Sql("INSERT INTO Autor(NomeAutor) VALUES('Machado de Assis')");
            migrationBuilder.Sql("INSERT INTO Autor(NomeAutor) VALUES('José Saramago')");
            migrationBuilder.Sql("INSERT INTO Autor(NomeAutor) VALUES('Gabriel García Márquez')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Autor");
        }
    }
}
