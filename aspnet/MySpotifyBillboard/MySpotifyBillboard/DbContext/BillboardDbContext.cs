using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MySpotifyBillboard.Models;

namespace MySpotifyBillboard.DbContext
{
    public class BillboardDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BillboardDbContext(DbContextOptions<BillboardDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TopTrackList> TopTrackLists { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<TrackArtist> TrackArtists { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackArtist>()
                .HasKey(ta => new {ta.TrackId, ta.ArtistId});

            modelBuilder.Entity<TrackArtist>()
                .HasOne(ta => ta.Track)
                .WithMany(t => t.TrackArtists)
                .HasForeignKey(ta => ta.TrackId);

            modelBuilder.Entity<TrackArtist>()
                .HasOne(ta => ta.Artist)
                .WithMany(a => a.TrackArtists)
                .HasForeignKey(ta => ta.ArtistId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.TopTrackLists)
                .WithOne(ttl => ttl.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TopTrackList>()
                .HasMany(ttl => ttl.Tracks)
                .WithOne(t => t.TopTrackList)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);



        }

    }
}
