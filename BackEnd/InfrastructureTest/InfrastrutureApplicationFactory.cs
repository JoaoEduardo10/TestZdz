using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace InfrastrutureTest
{
   public class InfrastrutureApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
    {
        private readonly PostgreSqlContainer _PostgreSqlContainer = new PostgreSqlBuilder().Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<InfrastrutureDataBaseContext>));

                services.Remove(dbContextDescriptor!);

                services.AddDbContext<InfrastrutureDataBaseContext>(options =>
                {
                    options.UseNpgsql(_PostgreSqlContainer.GetConnectionString());
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _PostgreSqlContainer.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _PostgreSqlContainer.StopAsync();
        }

        public async Task ClearDataBaseAsync()
        {
            using var connnection = new NpgsqlConnection(_PostgreSqlContainer.GetConnectionString());
            await connnection.OpenAsync();
            var respawner = await Respawner.CreateAsync(connnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
            });

            await respawner.ResetAsync(connnection);
        }

    }
}
