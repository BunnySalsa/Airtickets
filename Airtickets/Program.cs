global using Airtickets.Data;
global using Microsoft.EntityFrameworkCore;
using Airtickets.Filters;
using Airtickets.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AirTicketsDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<SaleAsyncResourceFilter>();
builder.Services.AddScoped<RefundResourceFilter>();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});
builder.WebHost.ConfigureKestrel(c => { c.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(3); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AirTickets V1",
        Description = "First version"
    });
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "API V2",
        Description = "Second version"
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", "API V1");
        c.SwaggerEndpoint($"/swagger/v2/swagger.json", "API V2");
    });
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();