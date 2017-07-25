using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySpotifyBillboard.DbContext;

namespace MySpotifyBillboard.Migrations
{
    [DbContext(typeof(BillboardDbContext))]
    [Migration("20170724150227_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("TokenType");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
