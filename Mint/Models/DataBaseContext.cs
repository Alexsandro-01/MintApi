using System;
using Microsoft.EntityFrameworkCore;
using Mint.Models;

public class DataBaseContext : DbContext
{
  public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
  {}

  public DbSet<Unit> Units { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

      optionsBuilder.UseNpgsql(connectionString+"Database=mint;");
    }
  }
} 