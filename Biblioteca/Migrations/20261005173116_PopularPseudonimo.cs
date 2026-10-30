using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class PopularPseudonimo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Pseudonimo(AutorId, NomePseudonimo) VALUES(1, 'George Orwell')");
            migrationBuilder.Sql("INSERT INTO Pseudonimo(AutorId, NomePseudonimo) VALUES(2, 'Mark Twain')");
            migrationBuilder.Sql("INSERT INTO Pseudonimo(AutorId, NomePseudonimo) VALUES(3, 'George Eliot')");
            migrationBuilder.Sql("INSERT INTO Pseudonimo(AutorId, NomePseudonimo) VALUES(4, 'Lewis Carroll')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Pseudonimo");
        }
    }
}
