using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Frasaria.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Frases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etiquetas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favoritas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FraseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoritas_Frases_FraseId",
                        column: x => x.FraseId,
                        principalTable: "Frases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favoritas_FraseId",
                table: "Favoritas",
                column: "FraseId");

            migrationBuilder.CreateIndex(
                name: "IX_Frases_UserId",
                table: "Frases",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritas");

            migrationBuilder.DropTable(
                name: "Frases");
        }
    }
}
