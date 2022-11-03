using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using ShareVideosAPI.DependencyInjection;
using ShareVideosAPI.Middlewares.Errors;
using ShareVideosAPI.Middlewares.Filters;
using ShareVideosAPIatabase;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
     {
         options.SuppressModelStateInvalidFilter = true;
     })
    .AddJsonOptions(
        options =>
        {
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.SchemaFilter<EnumSchemaFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Share Videos API",
    });
});

builder.Services.AddInfrastructureDB(builder.Configuration);
builder.Services.AddAutoMapper();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
