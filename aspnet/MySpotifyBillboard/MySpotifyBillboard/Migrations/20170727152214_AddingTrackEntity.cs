using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MySpotifyBillboard.Migrations
{
    public partial class AddingTrackEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlbumId = table.Column<string>(nullable: false),
                    AlbumName = table.Column<string>(nullable: false),
                    LargeImage = table.Column<string>(nullable: false),
                    MediumImage = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OpenInSpotify = table.Column<string>(nullable: false),
                    PreviousPosition = table.Column<string>(nullable: true),
                    SmallImage = table.Column<string>(nullable: false),
                    SpotifyTrackId = table.Column<string>(nullable: false),
                    TimeAtNumberOne = table.Column<int>(nullable: false),
                    TimeOnChart = table.Column<int>(nullable: false),
                    TopTrackListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_Tracks_TopTrackLists_TopTrackListId",
                        column: x => x.TopTrackListId,
                        principalTable: "TopTrackLists",
                        principalColumn: "TopTrackListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_TopTrackListId",
                table: "Tracks",
                column: "TopTrackListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tracks");
        }
    }
}
