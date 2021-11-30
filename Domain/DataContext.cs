using Domain.Models.Group;
using Domain.Models.Survey;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<StudentModel> Students { get; set; }

        public DbSet<SurveyModel> Surveys { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionMessage> QuestionMessages { get; set; }
        public DbSet<OptionModel> Options { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }
        public DbSet<ImageModel> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GroupModel>().HasMany(g => g.Students).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SurveyModel>().HasMany(s => s.Questions).WithOne().HasForeignKey(q => q.SurveyId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuestionModel>().HasMany<QuestionMessage>().WithOne(qm => qm.Question).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OptionModel>().HasOne(o => o.Question).WithMany(q => q.Options).HasForeignKey(o => o.QuestionId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuestionMessage>().HasOne<AnswerModel>().WithOne(a => a.QuestionMessage).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<QuestionMessage>().HasOne(qm => qm.Student).WithMany().HasForeignKey(qm => qm.StudentId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AnswerModel>().HasOne(a => a.Option).WithMany().HasForeignKey(a => a.OptionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}