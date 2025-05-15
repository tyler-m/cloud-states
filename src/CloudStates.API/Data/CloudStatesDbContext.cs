using Microsoft.EntityFrameworkCore;
using CloudStates.API.Models;

namespace CloudStates.API.Data
{
    internal class CloudStatesDbContext(DbContextOptions<CloudStatesDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<SaveState> SaveStates => Set<SaveState>();
        public DbSet<PreSignedUrl> PreSignedUrls => Set<PreSignedUrl>();

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
