using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Testcontainers.MsSql;

namespace InfrastrutureTest
{
   public class InfrastrutureApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
    {
        private readonly MsSqlContainer  _SqlServerContainer = new MsSqlBuilder()
            .WithEnvironment("ACCEPT_EULA", "Y")
            .Build();

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
                    options.UseSqlServer(_SqlServerContainer.GetConnectionString());
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _SqlServerContainer.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _SqlServerContainer.StopAsync();
        }

        public async Task ClearDataBaseAsync()
        {
            using var connnection = new NpgsqlConnection(_SqlServerContainer.GetConnectionString());
            await connnection.OpenAsync();
            var respawner = await Respawner.CreateAsync(connnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
            });

            await respawner.ResetAsync(connnection);
        }

    }
}
