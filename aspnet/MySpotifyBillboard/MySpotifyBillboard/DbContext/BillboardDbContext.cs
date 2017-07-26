using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySpotifyBillboard.Models;

namespace MySpotifyBillboard.DbContext
{
    public class BillboardDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BillboardDbContext(DbContextOptions<BillboardDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
