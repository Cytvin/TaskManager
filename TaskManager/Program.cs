using TaskManagerAPI.EFModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Строка подключения 'DefaultConnections' не найдена");
builder.Services.AddDbContext<TaskManagerDBContext>(options =>
    options.UseSqlServer(connectionStrings));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
