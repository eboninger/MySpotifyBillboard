using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySpotifyBillboard.DbContext;

namespace MySpotifyBillboard.Migrations
{
    [DbContext(typeof(BillboardDbContext))]
    [Migration("20170726203701_AddingTrackCharts")]
    partial class AddingTrackCharts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MySpotifyBillboard.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtistId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OpenInSpotify")
                        .IsRequired();

                    b.Property<int?>("TrackId");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.TopTrackList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TopTrackList");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlbumId")
                        .IsRequired();

                    b.Property<string>("AlbumName")
                        .IsRequired();

                    b.Property<string>("LargeImage")
                        .IsRequired();

                    b.Property<string>("MediumImage")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OpenInSpotify")
                        .IsRequired();

                    b.Property<string>("PreviousPosition");

                    b.Property<string>("SmallImage")
                        .IsRequired();

                    b.Property<int>("TimeAtNumberOne");

                    b.Property<int>("TimeOnChart");

                    b.Property<int?>("TopTrackListId");

                    b.Property<string>("TrackId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TopTrackListId");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessToken");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("ExpirationTime");

                    b.Property<int?>("FourWeekTopTracksId");

                    b.Property<string>("RefreshToken")
                        .IsRequired();

                    b.Property<string>("Scope")
                        .IsRequired();

                    b.Property<int?>("SixMonthTopTracksId");

                    b.Property<string>("SpotifyId")
                        .IsRequired();

                    b.Property<string>("TokenType");

                    b.HasKey("Id");

                    b.HasIndex("FourWeekTopTracksId");

                    b.HasIndex("SixMonthTopTracksId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.Artist", b =>
                {
                    b.HasOne("MySpotifyBillboard.Models.Track")
                        .WithMany("Artists")
                        .HasForeignKey("TrackId");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.TopTrackList", b =>
                {
                    b.HasOne("MySpotifyBillboard.Models.User")
                        .WithOne("AllTimeTopTracks")
                        .HasForeignKey("MySpotifyBillboard.Models.TopTrackList", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.Track", b =>
                {
                    b.HasOne("MySpotifyBillboard.Models.TopTrackList")
                        .WithMany("Tracks")
                        .HasForeignKey("TopTrackListId");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.User", b =>
                {
                    b.HasOne("MySpotifyBillboard.Models.TopTrackList", "FourWeekTopTracks")
                        .WithMany()
                        .HasForeignKey("FourWeekTopTracksId");

                    b.HasOne("MySpotifyBillboard.Models.TopTrackList", "SixMonthTopTracks")
                        .WithMany()
                        .HasForeignKey("SixMonthTopTracksId");
                });
        }
    }
}
