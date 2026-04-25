using API.Configuration;
using API.Middlewares;
using Application.Extensions;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Infrastructure.EF_Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services
            .AddApiVersioning(options => {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

        builder.Services.AddDbContext<SpidermanContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DB_SPIDERMAN")));

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        //Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions) {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        $"Spiderman API {description.GroupName.ToUpper()}"
                    );
                }
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // app.MapGet("/", () => "Spiderman");
        app.Run();
    }
}