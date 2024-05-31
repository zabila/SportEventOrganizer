using MediatR;
using Sportalytics.API.Feed.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    config.RegisterServicesFromAssemblies(Sportalytics.Application.Feed.AssemblyReference.Assembly);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();