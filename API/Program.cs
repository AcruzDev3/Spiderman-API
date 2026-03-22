using LIB.Extensions;
using LIB.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SpidermanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB_SPIDERMAN")));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

//Configure the HTTP request pipeline.
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
