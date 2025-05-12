using CloudStates.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudStates.API.Data
{
    internal class CloudStatesDbContext(DbContextOptions<CloudStatesDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<SaveState> SaveStates => Set<SaveState>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // save state and user relationship
            modelBuilder.Entity<SaveState>()
                .HasOne(ss => ss.User)
                .WithMany(u => u.SaveStates)
                .HasForeignKey(ss => ss.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
