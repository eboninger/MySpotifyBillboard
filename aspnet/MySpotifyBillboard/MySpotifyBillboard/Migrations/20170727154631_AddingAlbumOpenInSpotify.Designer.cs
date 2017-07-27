using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySpotifyBillboard.DbContext;
using MySpotifyBillboard.Models;

namespace MySpotifyBillboard.Migrations
{
    [DbContext(typeof(BillboardDbContext))]
    [Migration("20170727154631_AddingAlbumOpenInSpotify")]
    partial class AddingAlbumOpenInSpotify
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MySpotifyBillboard.Models.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OpenInSpotify")
                        .IsRequired();

                    b.Property<string>("SpotifyArtistId")
                        .IsRequired();

                    b.HasKey("ArtistId");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.TopTrackList", b =>
                {
                    b.Property<int>("TopTrackListId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TimeFrame");

                    b.Property<int?>("UserId");

                    b.HasKey("TopTrackListId");

                    b.HasIndex("UserId");

                    b.ToTable("TopTrackLists");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlbumId")
                        .IsRequired();

                    b.Property<string>("AlbumName")
                        .IsRequired();

                    b.Property<string>("AlbumOpenInSpotify")
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

                    b.Property<string>("SpotifyTrackId")
                        .IsRequired();

                    b.Property<int>("TimeAtNumberOne");

                    b.Property<int>("TimeOnChart");

                    b.Property<int?>("TopTrackListId")
                        .IsRequired();

                    b.HasKey("TrackId");

                    b.HasIndex("TopTrackListId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.TrackArtist", b =>
                {
                    b.Property<int>("TrackId");

                    b.Property<int>("ArtistId");

                    b.HasKey("TrackId", "ArtistId");

                    b.HasIndex("ArtistId");

                    b.ToTable("TrackArtists");
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

                    b.Property<string>("RefreshToken")
                        .IsRequired();

                    b.Property<string>("Scope")
                        .IsRequired();

                    b.Property<string>("SpotifyId")
                        .IsRequired();

                    b.Property<string>("TokenType");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.TopTrackList", b =>
                {
                    b.HasOne("MySpotifyBillboard.Models.User", "User")
                        .WithMany("TopTrackLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.Track", b =>
                {
                    b.HasOne("MySpotifyBillboard.Models.TopTrackList", "TopTrackList")
                        .WithMany("Tracks")
                        .HasForeignKey("TopTrackListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MySpotifyBillboard.Models.TrackArtist", b =>
                {
                    b.HasOne("MySpotifyBillboard.Models.Artist", "Artist")
                        .WithMany("TrackArtists")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MySpotifyBillboard.Models.Track", "Track")
                        .WithMany("TrackArtists")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
