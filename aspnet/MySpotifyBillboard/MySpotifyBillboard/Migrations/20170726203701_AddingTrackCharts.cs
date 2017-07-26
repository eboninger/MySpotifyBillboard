using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MySpotifyBillboard.Migrations
{
    public partial class AddingTrackCharts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FourWeekTopTracksId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SixMonthTopTracksId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TopTrackList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopTrackList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopTrackList_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlbumId = table.Column<string>(nullable: false),
                    AlbumName = table.Column<string>(nullable: false),
                    LargeImage = table.Column<string>(nullable: false),
                    MediumImage = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OpenInSpotify = table.Column<string>(nullable: false),
                    PreviousPosition = table.Column<string>(nullable: true),
                    SmallImage = table.Column<string>(nullable: false),
                    TimeAtNumberOne = table.Column<int>(nullable: false),
                    TimeOnChart = table.Column<int>(nullable: false),
                    TopTrackListId = table.Column<int>(nullable: true),
                    TrackId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Track_TopTrackList_TopTrackListId",
                        column: x => x.TopTrackListId,
                        principalTable: "TopTrackList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArtistId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OpenInSpotify = table.Column<string>(nullable: false),
                    TrackId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artist_Track_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Track",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_FourWeekTopTracksId",
                table: "Users",
                column: "FourWeekTopTracksId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SixMonthTopTracksId",
                table: "Users",
                column: "SixMonthTopTracksId");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_TrackId",
                table: "Artist",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_TopTrackList_UserId",
                table: "TopTrackList",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Track_TopTrackListId",
                table: "Track",
                column: "TopTrackListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TopTrackList_FourWeekTopTracksId",
                table: "Users",
                column: "FourWeekTopTracksId",
                principalTable: "TopTrackList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TopTrackList_SixMonthTopTracksId",
                table: "Users",
                column: "SixMonthTopTracksId",
                principalTable: "TopTrackList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TopTrackList_FourWeekTopTracksId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_TopTrackList_SixMonthTopTracksId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "Track");

            migrationBuilder.DropTable(
                name: "TopTrackList");

            migrationBuilder.DropIndex(
                name: "IX_Users_FourWeekTopTracksId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SixMonthTopTracksId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FourWeekTopTracksId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SixMonthTopTracksId",
                table: "Users");
        }
    }
}
