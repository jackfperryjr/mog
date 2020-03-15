using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Data.Migrations
{
    public partial class RemovedPropsFromCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Picture2",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Picture3",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Picture4",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Picture5",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response1",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response10",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response2",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response3",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response4",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response5",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response6",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response7",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response8",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Response9",
                table: "Character");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture2",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture3",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture4",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture5",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response1",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response10",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response2",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response3",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response4",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response5",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response6",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response7",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response8",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response9",
                table: "Character",
                nullable: true);
        }
    }
}
