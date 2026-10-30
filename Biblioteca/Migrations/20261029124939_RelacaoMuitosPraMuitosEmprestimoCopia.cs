using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class RelacaoMuitosPraMuitosEmprestimoCopia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copia_Emprestimo_EmprestimoId",
                table: "Copia");

            migrationBuilder.DropIndex(
                name: "IX_Copia_EmprestimoId",
                table: "Copia");

            migrationBuilder.DropColumn(
                name: "EmprestimoId",
                table: "Copia");

            migrationBuilder.CreateTable(
                name: "CopiaEmprestimo",
                columns: table => new
                {
                    CopiasCopiaId = table.Column<int>(type: "int", nullable: false),
                    EmprestimosEmprestimoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopiaEmprestimo", x => new { x.CopiasCopiaId, x.EmprestimosEmprestimoId });
                    table.ForeignKey(
                        name: "FK_CopiaEmprestimo_Copia_CopiasCopiaId",
                        column: x => x.CopiasCopiaId,
                        principalTable: "Copia",
                        principalColumn: "CopiaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CopiaEmprestimo_Emprestimo_EmprestimosEmprestimoId",
                        column: x => x.EmprestimosEmprestimoId,
                        principalTable: "Emprestimo",
                        principalColumn: "EmprestimoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CopiaEmprestimo_EmprestimosEmprestimoId",
                table: "CopiaEmprestimo",
                column: "EmprestimosEmprestimoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CopiaEmprestimo");

            migrationBuilder.AddColumn<int>(
                name: "EmprestimoId",
                table: "Copia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Copia_EmprestimoId",
                table: "Copia",
                column: "EmprestimoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copia_Emprestimo_EmprestimoId",
                table: "Copia",
                column: "EmprestimoId",
                principalTable: "Emprestimo",
                principalColumn: "EmprestimoId");
        }
    }
}
