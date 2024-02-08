using GarageEvents.Extensions;
using GarageEvents.Nats.Extensions;
using GarageEvents.Remote;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddGarageComponents()
  .AddNatsRemote(builder.Configuration);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()));

if (builder.Environment.IsProduction())
{
  _ = builder.WebHost.UseSetting("http_port", "80");
  _ = builder.WebHost.UseSetting("https_port", "443");
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
}
_ = app.UseSwagger();
_ = app.UseSwaggerUI();

//app.UseHttpsRedirection();

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

app.MapPost("/refresh", async (IRemote handler) =>
{
  await handler.Refresh();
})
.WithName("Refresh")
.WithOpenApi();

app.Run();
