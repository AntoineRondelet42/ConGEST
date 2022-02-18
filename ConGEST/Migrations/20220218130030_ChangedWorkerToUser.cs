using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConGEST.Migrations
{
    public partial class ChangedWorkerToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holliday_Worker_WorkerId",
                table: "Holliday");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Worker_WorkerId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Service_WorkerId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Holliday_WorkerId",
                table: "Holliday");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Holliday");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Service",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Holliday",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ValidState",
                columns: new[] { "ValidStateId", "ValidStateLib" },
                values: new object[] { 1, "En attente" });

            migrationBuilder.InsertData(
                table: "ValidState",
                columns: new[] { "ValidStateId", "ValidStateLib" },
                values: new object[] { 2, "Acceptée" });

            migrationBuilder.InsertData(
                table: "ValidState",
                columns: new[] { "ValidStateId", "ValidStateLib" },
                values: new object[] { 3, "Refusée" });

            migrationBuilder.CreateIndex(
                name: "IX_Service_UserId",
                table: "Service",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Holliday_UserId",
                table: "Holliday",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holliday_AspNetUsers_UserId",
                table: "Holliday",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_AspNetUsers_UserId",
                table: "Service",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holliday_AspNetUsers_UserId",
                table: "Holliday");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_AspNetUsers_UserId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_UserId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Holliday_UserId",
                table: "Holliday");

            migrationBuilder.DeleteData(
                table: "ValidState",
                keyColumn: "ValidStateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ValidState",
                keyColumn: "ValidStateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ValidState",
                keyColumn: "ValidStateId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Holliday");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Holliday",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    WorkerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.WorkerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_WorkerId",
                table: "Service",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Holliday_WorkerId",
                table: "Holliday",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holliday_Worker_WorkerId",
                table: "Holliday",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Worker_WorkerId",
                table: "Service",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
