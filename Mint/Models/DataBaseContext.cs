using System;
using Microsoft.EntityFrameworkCore;
using Mint.Models;

public class DataBaseContext : DbContext
{
  public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
  {}

  public DbSet<Customer> Customers { get; set; }
  public DbSet<Entry> Entries { get; set; }
  public DbSet<Expense> Expenses { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Supplier> Suppliers { get; set; } 
  public DbSet<Unit> Units { get; set; }
  public DbSet<User> Users { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

      optionsBuilder.UseNpgsql(connectionString+"Database=mint;");
    }
  }
} 