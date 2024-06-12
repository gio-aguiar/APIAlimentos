using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace alimentosAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Alimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Calorias = table.Column<int>(type: "int", nullable: false),
                    Carboidratos = table.Column<int>(type: "int", nullable: false),
                    Proteinas = table.Column<int>(type: "int", nullable: false),
                    Gorduras = table.Column<int>(type: "int", nullable: false),
                    Fibras = table.Column<int>(type: "int", nullable: false),
                    Sodio = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Alimentos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TB_Alimentos",
                columns: new[] { "Id", "Calorias", "Carboidratos", "Fibras", "Gorduras", "Nome", "Proteinas", "Sodio", "Tipo" },
                values: new object[,]
                {
                    { 1, 56, 15, 1, 0, "Maçã", 0, 0, 1 },
                    { 2, 98, 26, 2, 0, "Banana", 1, 21, 1 },
                    { 3, 207, 1, 0, 11, "Picanha", 20, 450, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Alimentos");
        }
    }
}
