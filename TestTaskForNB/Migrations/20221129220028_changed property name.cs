using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTaskForNB.Migrations
{
    /// <inheritdoc />
    public partial class changedpropertyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatingDate",
                table: "Posts",
                newName: "PostCreatingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostCreatingDate",
                table: "Posts",
                newName: "CreatingDate");
        }
    }
}
