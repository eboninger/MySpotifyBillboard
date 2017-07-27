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
    [Migration("20170727151920_AddingTopTrackList")]
    partial class AddingTopTrackList
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
        }
    }
}
