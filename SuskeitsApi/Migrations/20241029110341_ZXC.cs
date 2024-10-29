using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuskeitsApi.Migrations
{
    /// <inheritdoc />
    public partial class ZXC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tooted_Kasutajad_KasutajaId",
                table: "Tooted");

            migrationBuilder.DropIndex(
                name: "IX_Tooted_KasutajaId",
                table: "Tooted");

            migrationBuilder.DropColumn(
                name: "KasutajaId",
                table: "Tooted");

            migrationBuilder.CreateTable(
                name: "KasutajaTooded",
                columns: table => new
                {
                    KasutajaId = table.Column<int>(type: "int", nullable: false),
                    ToodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KasutajaTooded", x => new { x.KasutajaId, x.ToodeId });
                    table.ForeignKey(
                        name: "FK_KasutajaTooded_Kasutajad_KasutajaId",
                        column: x => x.KasutajaId,
                        principalTable: "Kasutajad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KasutajaTooded_Tooted_ToodeId",
                        column: x => x.ToodeId,
                        principalTable: "Tooted",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_KasutajaTooded_ToodeId",
                table: "KasutajaTooded",
                column: "ToodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KasutajaTooded");

            migrationBuilder.AddColumn<int>(
                name: "KasutajaId",
                table: "Tooted",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tooted_KasutajaId",
                table: "Tooted",
                column: "KasutajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tooted_Kasutajad_KasutajaId",
                table: "Tooted",
                column: "KasutajaId",
                principalTable: "Kasutajad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
