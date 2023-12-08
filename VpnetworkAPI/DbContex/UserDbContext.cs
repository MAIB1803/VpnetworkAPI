using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.Models;

namespace VpnetworkAPI.DbContex
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GlobalProgramData> GlobalData { get; set; }
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
        // Add other DbSet properties for ProgramData, Settings, LocalProgramData, and ThresholdSettings if needed


    }
}
