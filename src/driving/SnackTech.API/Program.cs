using SnackTech.API.Configuration.HealthChecks;
using SnackTech.Application;
using SnackTech.Adapter.DataBase;
using SnackTech.Adapter.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAdapterDatabaseRepositories();
builder.Services.AddApplicationServices();

builder.Services.AddHealthChecks()
                .ConfigureSQLHealthCheck(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerGen(c => {
    c.DescribeAllParametersInCamelCase();
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
await dbContext.Database.MigrateAsync();

app.UseCustomHealthChecks();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
