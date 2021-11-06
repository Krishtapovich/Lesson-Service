using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected DataContext()
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>(m => m.Property(s => s.Id).ValueGeneratedOnAdd());
            builder.Entity<Student>().HasOne(s => s.Group).WithMany(g => g.Students).HasForeignKey(s => s.GroupId);
        }
    }
}