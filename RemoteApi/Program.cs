using GarageEvents.Extensions;
using GarageEvents.Nats.Extensions;
using GarageEvents.Remote;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  _ = app.UseSwagger();
  _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/opendoor", async (IRemote handler) =>
{
  await handler.OpenDoor();
})
.WithName("OpenDoor")
.WithOpenApi();

app.MapPost("/lightson", async (IRemote handler) =>
{
  await handler.LightsOn();
})
.WithName("LightsOn")
.WithOpenApi();

app.MapPost("/lightsoff", async (IRemote handler) =>
{
  await handler.LightsOff();
})
.WithName("LightsOff")
.WithOpenApi();

app.MapPost("/closedoor", async (IRemote handler) =>
{
  await handler.CloseDoor();
})
.WithName("CloseDoor")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
