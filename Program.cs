using Microsoft.EntityFrameworkCore;
using Quickpass.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine("===== CONNECTION STRING =====");
Console.WriteLine(connectionString);
Console.WriteLine("=============================");

builder.Services.AddDbContext<QuickPassContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000","https://quickpass-production.up.railway.app")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();