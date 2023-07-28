using MCLauncher.Core.Entities;
using MCLauncher.DataAcess.Model;
using MCLauncher.SecurityService.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MSLContext>(optionsBuilder =>
{
    var cns = builder.Configuration.GetConnectionString("MSL_DB");
    optionsBuilder.UseSqlServer(cns);
});

var app = builder.Build();
Statics_Core.Configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
