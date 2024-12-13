using Microsoft.EntityFrameworkCore;

public class DataBaseContext : DbContext
{
  public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
  {

  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

      optionsBuilder.UseNpgsql(connectionString);
    }
  }
} 