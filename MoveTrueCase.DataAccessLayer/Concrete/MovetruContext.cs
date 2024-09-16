using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovetruCase.Entities.Entities;
using MovetruCaseEntities.Entities;

namespace MovetruCaseDataAccessLayer.Concrete
{
    public class MovetruContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=MovetruCase;Trusted_Connection=True;TrustServerCertificate=true;");

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyStepLog>().HasKey(dl => new { dl.UserId, dl.CreationDate });
            modelBuilder.Entity<UserData>().HasKey(userData => userData.UserID);

            modelBuilder.Entity<UserData>().HasMany(userData => userData.DailyStepLogs)
                .WithOne(dailyStepLog => dailyStepLog.UserData).HasForeignKey(dailyStepLog => dailyStepLog.UserId);
        }

        #region DbSets

        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<DailyStepLog> DailyStepLogs { get; set; }
        #endregion
    }
}
