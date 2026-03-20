using LIB.Models;
using LIB.Managers;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<SpidermanContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DB_SPIDERMAN")));

builder.Services.AddScoped<CrimeManager>();
builder.Services.AddScoped<AddressManager>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<CriminalManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Spiderman");
app.Run();
