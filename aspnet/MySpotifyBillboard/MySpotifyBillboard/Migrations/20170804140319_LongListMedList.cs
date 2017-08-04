using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MySpotifyBillboard.Migrations
{
    public partial class LongListMedList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongTrackList",
                table: "Users",
                maxLength: 60000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedTrackList",
                table: "Users",
                maxLength: 60000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongTrackList",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MedTrackList",
                table: "Users");
        }
    }
}
