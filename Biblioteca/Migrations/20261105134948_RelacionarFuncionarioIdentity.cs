using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class RelacionarFuncionarioIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Funcionario",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_IdentityUserId",
                table: "Funcionario",
                column: "IdentityUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_AspNetUsers_IdentityUserId",
                table: "Funcionario",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_AspNetUsers_IdentityUserId",
                table: "Funcionario");

            migrationBuilder.DropIndex(
                name: "IX_Funcionario_IdentityUserId",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Funcionario");
        }
    }
}
