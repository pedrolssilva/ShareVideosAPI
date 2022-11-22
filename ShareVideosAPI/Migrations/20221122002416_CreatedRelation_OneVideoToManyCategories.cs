using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareVideosAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedRelationOneVideoToManyCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "videos",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_videos_CategoryId",
                table: "videos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_videos_categories_CategoryId",
                table: "videos",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_videos_categories_CategoryId",
                table: "videos");

            migrationBuilder.DropIndex(
                name: "IX_videos_CategoryId",
                table: "videos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "videos");
        }
    }
}
