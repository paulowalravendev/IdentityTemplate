using IdentityTemplate.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApiConfiguration(builder.Configuration);
builder.Build().UseWebApiConfiguration();