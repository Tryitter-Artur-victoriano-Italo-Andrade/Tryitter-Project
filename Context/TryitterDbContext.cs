using Microsoft.EntityFrameworkCore;
using Tryitter_Project.Models;

namespace Tryitter_Project.Context;

public class TryitterDbContext : DbContext
{
    public TryitterDbContext( DbContextOptions<TryitterDbContext> options) : base( options)
    { }

    public DbSet<Student> Students {get; set;}
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"
                Server=localhost;
                Database=Tryitter;
                User=SA;
                Password=0123456;
            ");
        }
    }
}