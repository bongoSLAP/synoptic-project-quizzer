using Microsoft.EntityFrameworkCore;

namespace Quizzer.Data;

public class QuizzerContext : DbContext
{
    public QuizzerContext (DbContextOptions<QuizzerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

    }
}