using Sportalytics.Feed.API.Extensions;
using Sportalytics.Feed.Application;
using Sportalytics.Feed.Infrastructure;
using Sportalytics.Feed.Infrastructure.Kafka;
using Sportalytics.Feed.Persistence.MongoDB;
using Sportalytics.Feed.Persistence.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddPostgreSql(builder.Configuration)
    .AddMongo(builder.Configuration)
    .AddApiSportKafkaConsumer(builder.Configuration)
    .AddApiSportsService(builder.Configuration);

var app = builder.Build();
app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureHandfireDashboard();
app.MapControllers();
app.Run();