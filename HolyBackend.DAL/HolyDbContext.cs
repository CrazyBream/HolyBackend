using HolyBackend.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyBackend.DAL
{
    public class HolyDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMessage> OrderMessages { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        public HolyDbContext(DbContextOptions<HolyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HolyDbContext).Assembly);
        }
    }
}