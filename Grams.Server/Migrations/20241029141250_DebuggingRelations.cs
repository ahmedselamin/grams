﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grams.Server.Migrations
{
    /// <inheritdoc />
    public partial class DebuggingRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Posts");
        }
    }
}
