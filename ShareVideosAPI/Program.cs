using Microsoft.EntityFrameworkCore;
using Npgsql;
using ShareVideosAPI.DependencyInjection;
using ShareVideosAPIatabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureDB(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<DbContextPostgre>();
        dataContext.Database.Migrate();

        using (var conn = (NpgsqlConnection)dataContext.Database.GetDbConnection())
        {
            conn.Open();
            conn.ReloadTypes();
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
