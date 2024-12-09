using Microsoft.EntityFrameworkCore;
using UpTimeMontior.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UpTimeDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevDb") ?? throw new InvalidOperationException("Connectoin string not found")));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); 

app.UseAuthorization();

app.MapControllers();

app.Run();
