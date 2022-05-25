using Microsoft.EntityFrameworkCore.Migrations;

namespace RedPill.Migrations
{
    public partial class atualizartabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuario",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "LancamentoId",
                table: "Lancamento",
                newName: "TransacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Usuario",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TransacaoId",
                table: "Lancamento",
                newName: "LancamentoId");
        }
    }
}
