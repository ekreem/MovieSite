using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetFlix.DAL.Migrations
{
    /// <inheritdoc />
    public partial class tmdbID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Movies",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Movies");
        }
    }
}
