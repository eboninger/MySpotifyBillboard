using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MySpotifyBillboard.Migrations
{
    public partial class AddingAlbumOpenInSpotify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumOpenInSpotify",
                table: "Tracks",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumOpenInSpotify",
                table: "Tracks");
        }
    }
}
