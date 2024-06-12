using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alimentosAPI.script03.TabelaUsuarios.sql
{
    /// <inheritdoc />
    public partial class MigracaoUsario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Classe",
                table: "TB_Alimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoPersonagem",
                table: "TB_Alimentos",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "TB_Alimentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_USUARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataAcesso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Perfil = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, defaultValue: "Jogador"),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIOS", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "TB_Alimentos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Classe", "FotoPersonagem", "UsuarioId" },
                values: new object[] { 0, null, 1 });

            migrationBuilder.UpdateData(
                table: "TB_Alimentos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Classe", "FotoPersonagem", "UsuarioId" },
                values: new object[] { 0, null, 1 });

            migrationBuilder.UpdateData(
                table: "TB_Alimentos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Classe", "FotoPersonagem", "UsuarioId" },
                values: new object[] { 0, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Alimentos_UsuarioId",
                table: "TB_Alimentos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Alimentos_TB_USUARIOS_UsuarioId",
                table: "TB_Alimentos",
                column: "UsuarioId",
                principalTable: "TB_USUARIOS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Alimentos_TB_USUARIOS_UsuarioId",
                table: "TB_Alimentos");

            migrationBuilder.DropTable(
                name: "TB_USUARIOS");

            migrationBuilder.DropIndex(
                name: "IX_TB_Alimentos_UsuarioId",
                table: "TB_Alimentos");

            migrationBuilder.DropColumn(
                name: "Classe",
                table: "TB_Alimentos");

            migrationBuilder.DropColumn(
                name: "FotoPersonagem",
                table: "TB_Alimentos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TB_Alimentos");
        }
    }
}
