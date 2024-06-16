using Sportalytics.Event.API.Extensions;
using Sportalytics.Event.Application;
using Sportalytics.Event.Infrastructure;
using Sportalytics.Event.Infrastructure.Kafka;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddApiSportsService(builder.Configuration)
    .AddKafkaProducer(builder.Configuration);

var app = builder.Build();
app.ConfigureExceptionHandler();
app.ConfigureHandfireDashboard();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();