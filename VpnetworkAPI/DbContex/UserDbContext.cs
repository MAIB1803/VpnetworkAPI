using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.Models;

namespace VpnetworkAPI.DbContex
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Analysis> Analyses { get; set; }
        public DbSet<LocalProgramData> LocalProgramData { get; set; }
        public DbSet<ProgramData> ProgramData { get; set; }
        public DbSet<ThresholdSettings> ThresholdSettings { get; set; }
        public DbSet<GlobalProgramData> GlobalProgramData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Analyses - User Relationship
            modelBuilder.Entity<Analysis>()
                .HasOne(a => a.User)
                .WithMany(u => u.Analysis)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // LocalProgramData - User Relationship
            modelBuilder.Entity<LocalProgramData>()
                .HasOne(lp => lp.User)
                .WithMany(u => u.LocalProgramData)
                .HasForeignKey(lp => lp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProgramData - User Relationship
            modelBuilder.Entity<ProgramData>()
                .HasOne(pd => pd.User)
                .WithMany(u => u.ProgramData)
                .HasForeignKey(pd => pd.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ThresholdSetting - User Relationship
            modelBuilder.Entity<ThresholdSettings>()
                .HasOne(ts => ts.User)
                .WithMany(u => u.ThresholdSettings)
                .HasForeignKey(ts => ts.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add other relationships and constraints as needed

            base.OnModelCreating(modelBuilder);
        }
    }
}
