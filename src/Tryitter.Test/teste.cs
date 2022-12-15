using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Tryitter_Project.Context;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Tryitter_Project.Test;
public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var dbContextDescriptor = services.SingleOrDefault(
              d => d.ServiceType ==
                  typeof(DbContextOptions<TryitterDbContext>));

      services.Remove(dbContextDescriptor);

      var dbConnectionDescriptor = services.SingleOrDefault(
              d => d.ServiceType ==
                  typeof(DbConnection));

      services.Remove(dbConnectionDescriptor);

      // Create open SqliteConnection so EF won't automatically close it.
      services.AddSingleton<DbConnection>(container =>
          {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
          });

      services.AddDbContext<TryitterDbContext>(options =>
          {
            options.UseInMemoryDatabase("TryitterDbTest");
          });
    });

    builder.UseEnvironment("Development");
  }
}