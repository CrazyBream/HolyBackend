using HolyBackend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyBackend.DAL
{
    public class HolyDbContext : DbContext
    {
        public HolyDbContext(DbContextOptions<HolyDbContext> options) : base(options) { }

        public DbSet<PrayerRequest> Prayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("holy_backend");
            base.OnModelCreating(modelBuilder);
        }
    }
}