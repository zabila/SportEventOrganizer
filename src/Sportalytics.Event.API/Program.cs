using Sportalytics.Event.API.Extensions;
using Sportalytics.Event.Application;
using Sportalytics.Event.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddApplication()
    .AddApiSportsService(builder.Configuration);

var app = builder.Build();
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.Run();