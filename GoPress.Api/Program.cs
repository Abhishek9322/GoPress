using GoPress.Api.Configurations;
using GoPress.Api.Middleware;
using GoPress.Application;
using GoPress.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();     // Swagger Configuration DI

builder.Services.AddJwtAuthentication(builder.Configuration);     // JWT Authentication DI

builder.Services.AddApplicationService();     // Application Layer DI

builder.Services.AddInfrastructureServices(builder.Configuration);     // Infrastructure Layer DI

//builder.Services.AddAplicationService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();
