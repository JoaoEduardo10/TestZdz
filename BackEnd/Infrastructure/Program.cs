using Infrastructure.Config;
using Infrastructure.Data;
using Infrastructure.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.ScopedConfig();

builder.Services.AddDbContext<InfrastrutureDataBaseContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDataBase"))
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(cors =>
{
    cors.AllowAnyOrigin();
    cors.AllowAnyHeader();
    cors.AllowAnyMethod();
}
);

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
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware(typeof(OrderErrorHandlingMiddleware));
app.UseMiddleware(typeof(ProductErrorHandlingMiddleware));

app.MapControllers();

app.Run();
