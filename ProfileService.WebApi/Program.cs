using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ProfileService.Application.Common;
using ProfileService.Data;
using ProfileService.WebApi.Extensions;
using ProfileService.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();
builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.Converters.Add(new JsonStringEnumMemberConverter());
    op.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddPostgresDbContext(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddHealthChecks().AddCheck<PostgresHealthCheck>("PostgresHealthCheck");

var app = builder.Build();
await app.Services.ApplyMigrationAsync();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "ProfileService API V1");
    options.DisplayRequestDuration();
    options.EnableTryItOutByDefault();
});

app.MapHealthChecks("/healthz");
app.MapHealthChecks("/readyz", new HealthCheckOptions());
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseGlobalExceptionHandler();

app.Run();