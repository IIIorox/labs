using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final.Models;
using Microsoft.EntityFrameworkCore;

namespace final.Database
{
    class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> Tasks { get; set; }

        private static readonly string _dbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.db");

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public static void EnsureDatabase()
        {
            using var db = new AppDbContext();

            if (!File.Exists(_dbPath))
                db.Database.Migrate();
            else
            {
                var pendingMigrations = db.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    db.Database.Migrate();
                }
            }
        }
    }
}
