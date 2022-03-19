using FluentValidation.AspNetCore;
using IdentityTemplate.Api.Settings;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace IdentityTemplate.Api.Configurations;

public static class WebApiConfiguration
{
    public static void AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"))
            .AddControllers()
            .AddFluentWithSwaggerConfiguration()
            .AddEndpointsApiExplorer()
            .AddSwaggerConfiguration();
    }

    public static void UseWebApiConfiguration(this WebApplication app)
    {
        app.UseSwaggerConfiguration();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    #region FluentWithSwagger
    private static IServiceCollection AddFluentWithSwaggerConfiguration(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<Program>())
            .Services.AddFluentValidationRulesToSwagger();
    }
    #endregion

    #region Swagger
    private static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Identity API",
                Description = "Web API for Identity API",
                Contact = new OpenApiContact
                {
                    Name = "Paulo Walraven",
                    Email = "pmwc04@gmail.com",
                    Url = new Uri("https://github.com/paulowalravendev")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
                TermsOfService = new Uri("https://example.com/terms")
            });
        });
    }

    private static void UseSwaggerConfiguration(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return;
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    #endregion
}