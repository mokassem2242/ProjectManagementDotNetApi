using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nullableAppuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Users_DomainUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DomainUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DomainUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("9cec1b6d-728c-4c96-9ee2-6a04aad60e8e"), "9cec1b6d-728c-4c96-9ee2-6a04aad60e8e", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApplicationUserId",
                table: "Users",
                column: "ApplicationUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ApplicationUserId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9cec1b6d-728c-4c96-9ee2-6a04aad60e8e"));

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "DomainUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DomainUserId",
                table: "AspNetUsers",
                column: "DomainUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Users_DomainUserId",
                table: "AspNetUsers",
                column: "DomainUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
