using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTaskForNB.Migrations
{
    /// <inheritdoc />
    public partial class Addedrubricmodel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rubrics_Posts_PostId",
                table: "Rubrics");

            migrationBuilder.DropIndex(
                name: "IX_Rubrics_PostId",
                table: "Rubrics");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Rubrics");

            migrationBuilder.CreateTable(
                name: "PostRubric",
                columns: table => new
                {
                    PostRubricsId = table.Column<int>(type: "int", nullable: false),
                    PostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRubric", x => new { x.PostRubricsId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_PostRubric_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostRubric_Rubrics_PostRubricsId",
                        column: x => x.PostRubricsId,
                        principalTable: "Rubrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostRubric_PostsId",
                table: "PostRubric",
                column: "PostsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostRubric");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Rubrics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_PostId",
                table: "Rubrics",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rubrics_Posts_PostId",
                table: "Rubrics",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
