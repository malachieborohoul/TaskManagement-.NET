using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignee_AspNetUsers_UserId",
                table: "Assignee");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignee_Tasks_TasksId",
                table: "Assignee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignee",
                table: "Assignee");

            migrationBuilder.RenameTable(
                name: "Assignee",
                newName: "Assignees");

            migrationBuilder.RenameIndex(
                name: "IX_Assignee_UserId",
                table: "Assignees",
                newName: "IX_Assignees_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignee_TasksId",
                table: "Assignees",
                newName: "IX_Assignees_TasksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignees",
                table: "Assignees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_AspNetUsers_UserId",
                table: "Assignees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_Tasks_TasksId",
                table: "Assignees",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_AspNetUsers_UserId",
                table: "Assignees");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_Tasks_TasksId",
                table: "Assignees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignees",
                table: "Assignees");

            migrationBuilder.RenameTable(
                name: "Assignees",
                newName: "Assignee");

            migrationBuilder.RenameIndex(
                name: "IX_Assignees_UserId",
                table: "Assignee",
                newName: "IX_Assignee_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignees_TasksId",
                table: "Assignee",
                newName: "IX_Assignee_TasksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignee",
                table: "Assignee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignee_AspNetUsers_UserId",
                table: "Assignee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignee_Tasks_TasksId",
                table: "Assignee",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
