using GarageEvents.Extensions;
using GarageEvents.Nats.Extensions;
using GarageEvents.Remote;
//TODO Make sure that remote is not listening, only sending messages
//TODO Make sure that garage is listening for status from door and light, only receiving messages
//TODO Make sure that light is listening to remote, but also can send message about current status
//TODO Make sure that door is listening to remote, but also can send message about current status
//TODO Maybe status should be sent frequently to the garage
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()));

//From NET8.0 they started to use port 8080 and 8081 as default ports for http and https
if (builder.Environment.IsProduction())
{
  _ = builder.WebHost.UseSetting("http_port", "80");
  _ = builder.WebHost.UseSetting("https_port", "443");
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
_ = app.UseSwagger();
_ = app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.MapPost("/opendoor", async (IRemote handler) =>
{
  //Inject the remote control to send message to garage and door
  await handler.OpenDoor();
})
.WithName("OpenDoor")
.WithOpenApi();

app.MapPost("/lightson", async (IRemote handler) =>
{
  //Inject the remote control to send message to garage and light
  await handler.LightsOn();
})
.WithName("LightsOn")
.WithOpenApi();

app.MapPost("/lightsoff", async (IRemote handler) =>
{
  //Inject the remote control to send message to garage and light
  await handler.LightsOff();
})
.WithName("LightsOff")
.WithOpenApi();

app.MapPost("/closedoor", async (IRemote handler) =>
{
  //Inject the remote control to send message to garage and door
  await handler.CloseDoor();
})
.WithName("CloseDoor")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
