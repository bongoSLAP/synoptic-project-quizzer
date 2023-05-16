using Microsoft.EntityFrameworkCore;
using Quizzer.Models.Entities;

namespace Quizzer.Data;

public class QuizzerContext : DbContext
{
    public DbSet<User> User { get; set; } = null!;
    public DbSet<Quiz> Quiz { get; set; } = null!;
    public DbSet<Question> Question { get; set; } = null!;
    public DbSet<Answer> Answer { get; set; } = null!;
    
    public QuizzerContext (DbContextOptions<QuizzerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Quiz>()
            .HasKey(q => q.Id);

        builder.Entity<Question>()
            .HasKey(q => q.Id);

        builder.Entity<Answer>()
            .HasKey(a => a.Id);

        builder.Entity<Question>()
            .HasOne(q => q.Quiz)
            .WithMany(qz => qz.Questions)
            .HasForeignKey(q => q.QuizId);

        builder.Entity<Answer>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId);
    }
}