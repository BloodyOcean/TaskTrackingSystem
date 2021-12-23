using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_AssignmentStatuses_AssignmentStatusId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Managers_ManagerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Histories_AssignmentStatusId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "AssignmentStatusId",
                table: "Histories");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Managers_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Managers_ManagerId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignmentStatusId",
                table: "Histories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_AssignmentStatusId",
                table: "Histories",
                column: "AssignmentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_AssignmentStatuses_AssignmentStatusId",
                table: "Histories",
                column: "AssignmentStatusId",
                principalTable: "AssignmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Managers_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
