using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tryitter_Project.Models;

namespace Tryitter_Project.Context;

public class TryitterDbContext : DbContext
{
  public TryitterDbContext(DbContextOptions<TryitterDbContext> options) : base(options)
  { }

  public DbSet<Post> Posts { get; set; }

  public DbSet<Student> Student { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
  }
}