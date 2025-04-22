using Dapper;

namespace Movies.Application.Database;

public class DbInitializer
{
  private readonly IDbConnectionFactory _dbConnectionFactory;

  public DbInitializer(IDbConnectionFactory dbConnectionFactory)
  {
    _dbConnectionFactory = dbConnectionFactory;
  }

  public async Task InitializeAsync(CancellationToken token)
  {
    using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
    
    await connection.ExecuteAsync("""
      create table if not exists movies (
        Id UUID primary key,
        Slug TEXT not null,
        Title TEXT not null,
        yearofrelease integer not null);
    """);
    
    await connection.ExecuteAsync("""
      create unique index concurrently if not exists movies_slug_uindex
      on movies 
      using btree(slug);
    """);
    
    await connection.ExecuteAsync("""
      create table if not exists genres (
        movieId UUID references movies (Id),
        name TEXT not null);
    """);
    
  }
  
}