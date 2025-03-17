using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicMvC.Migrations
{
    /// <inheritdoc />
    public partial class RemoveComicRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Comics_FirstAppearanceComicId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "ComicCharacters");

            migrationBuilder.DropTable(
                name: "ComicCreators");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Comics_FirstAppearanceComicId",
                table: "Characters",
                column: "FirstAppearanceComicId",
                principalTable: "Comics",
                principalColumn: "ComicId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Comics_FirstAppearanceComicId",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "ComicCharacters",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicCharacters", x => new { x.ComicId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_ComicCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComicCharacters_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "ComicId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComicCreators",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicCreators", x => new { x.ComicId, x.CreatorId });
                    table.ForeignKey(
                        name: "FK_ComicCreators_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "ComicId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComicCreators_Creators_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Creators",
                        principalColumn: "CreatorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicCharacters_CharacterId",
                table: "ComicCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicCreators_CreatorId",
                table: "ComicCreators",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Comics_FirstAppearanceComicId",
                table: "Characters",
                column: "FirstAppearanceComicId",
                principalTable: "Comics",
                principalColumn: "ComicId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
