using DriversExam.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriversExam.Infrastructure.Context;

public class MainContext : DbContext
{
    public DbSet<Answer> Answer { get; set; }
    public DbSet<Question> Question { get; set; }
    public DbSet<Image> Image { get; set; }
    
    public MainContext() : base() {}
    
    public MainContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=dbo.DriversExam.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>()
            .HasMany(x => x.Answers)
            .WithOne(x => x.Question)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .HasOne(x => x.CorrectAnswer);
        
        modelBuilder.Entity<Question>()
            .HasOne(x => x.Image)
            .WithOne(x => x.Question)
            .HasForeignKey<Image>(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}