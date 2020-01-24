using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace INSOLMIMARC.Academy.Api.Migrations
{
    public partial class Password : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Tutor",
                nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Tutor");


        }
    }
}
