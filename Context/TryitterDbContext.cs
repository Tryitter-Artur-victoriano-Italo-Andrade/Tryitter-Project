using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


using Tryitter_Project.Models;

namespace Tryitter_Project.Context;

public class TryitterDbContext : IdentityDbContext<IdentityUser>
{
  public TryitterDbContext(DbContextOptions<TryitterDbContext> options) : base(options)
  { }

  public DbSet<Post> Posts { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
  }

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