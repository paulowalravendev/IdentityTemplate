using IdentityTemplate.Api.Settings;

namespace IdentityTemplate.Api.Configurations;

public static class WebApiConfiguration
{
    public static void AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerConfiguration();
    }

    public static void UseWebApiConfiguration(this WebApplication app)
    {
        app.UseSwaggerConfiguration();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    #region Swagger
    private static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen();
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