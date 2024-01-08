using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.Models;

namespace VpnetworkAPI.DbContex
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Analysis> Analyses { get; set; }
        public DbSet<GlobalProgramData> GlobalData{ get; set; }
        public DbSet<ProgramData> ProgramData { get; set; }
        public DbSet<LocalProgramData> LocalProgramData { get; set; }
        public DbSet<ThresholdSettings> ThresholdSettings { get; set; }

        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - ProgramData relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.ProgramData)
                .WithOne(pd => pd.User)
                .HasForeignKey(pd => pd.UserId);

            // Unique constraint for ProgramData
            modelBuilder.Entity<ProgramData>()
    .HasIndex(pd => new { pd.UserId, pd.ProgramName })
    .IsUnique();


            // User - LocalProgramData relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.LocalProgramData)
                .WithOne(lpd => lpd.User)
                .HasForeignKey(lpd => lpd.UserId);

            // User - ThresholdSettings relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.ThresholdSettings)
                .WithOne(ts => ts.User)
                .HasForeignKey(ts => ts.UserId);
            modelBuilder.Entity<User>()
               .HasMany(u => u.Analyses)
               .WithOne(a => a.User)
               .HasForeignKey(a => a.UserId);

            // Setting GUID as the primary key for Analysis
            modelBuilder.Entity<Analysis>()
                .HasKey(a => a.AnalysisId);

        }
    }
}
