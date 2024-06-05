using Infrastructure.Config;
using Infrastructure.Data;
using Infrastructure.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy", build => 
    build.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());
});

builder.Services.AddControllers();

builder.Services.ScopedConfig();

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<InfrastrutureDataBaseContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("PostgresDataBase"))
);



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<InfrastrutureDataBaseContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database." + ex.Message);
    }
}

app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingDefaultProduct>().Seed();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware(typeof(OrderErrorHandlingMiddleware));
app.UseMiddleware(typeof(ProductErrorHandlingMiddleware));

app.MapControllers();

app.Run();

public partial class Program { }
