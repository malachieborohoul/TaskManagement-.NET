using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssigneeDbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_Tasks_TasksId",
                table: "Assignees");

            migrationBuilder.DropIndex(
                name: "IX_Assignees_TasksId",
                table: "Assignees");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "Assignees");

            migrationBuilder.CreateIndex(
                name: "IX_Assignees_TaskId",
                table: "Assignees",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_Tasks_TaskId",
                table: "Assignees",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_Tasks_TaskId",
                table: "Assignees");

            migrationBuilder.DropIndex(
                name: "IX_Assignees_TaskId",
                table: "Assignees");

            migrationBuilder.AddColumn<Guid>(
                name: "TasksId",
                table: "Assignees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Assignees_TasksId",
                table: "Assignees",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_Tasks_TasksId",
                table: "Assignees",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
