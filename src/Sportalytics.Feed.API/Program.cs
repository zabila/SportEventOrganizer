using Sportalytics.Feed.API.Extensions;
using Sportalytics.Feed.Application;
using Sportalytics.Feed.Persistence.MongoDB;
using Sportalytics.Feed.Persistence.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddPostgreSql(builder.Configuration)
    .AddMongo(builder.Configuration);

var app = builder.Build();
app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();