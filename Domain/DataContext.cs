using Domain.Models.Student;
using Domain.Models.Survey;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionMessage> QuestionMessages { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>().HasMany(g => g.Students).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Survey>().HasMany(s => s.Questions).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Question>().HasMany(q => q.Messages).WithOne(qm => qm.Question).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuestionMessage>().HasOne(qm => qm.Student).WithMany().HasForeignKey(qm => qm.StudentId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Option>().HasOne(o => o.Question).WithMany(q => q.Options).HasForeignKey(o => o.QuestionId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Answer>().HasOne(a => a.Option).WithMany().HasForeignKey(a => a.OptionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}