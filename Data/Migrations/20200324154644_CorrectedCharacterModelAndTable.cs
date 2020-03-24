﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mog.Data.Migrations
{
    public partial class CorrectedCharacterModelAndTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Character", schema: "dbo", newName: "Characters", newSchema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Characters", schema: "dbo", newName: "Character", newSchema: "dbo");
        }
    }
}
