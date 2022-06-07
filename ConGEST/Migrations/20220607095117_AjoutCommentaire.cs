using Microsoft.EntityFrameworkCore.Migrations;

namespace ConGEST.Migrations
{
    public partial class AjoutCommentaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Commentaire",
                table: "Holliday",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commentaire",
                table: "Holliday");
        }
    }
}
